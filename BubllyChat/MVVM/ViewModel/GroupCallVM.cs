using BubllyChat.Core;
using BubllyChat.Helper;
using BubllyChat.Service;
using Microsoft.MixedReality.WebRTC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BubllyChat.MVVM.ViewModel
{
    public class RemotePeerVM : ViewModelBase
    {
        public string ConnectionId { get; set; }

        private ImageSource _videoSource;
        public ImageSource VideoSource
        {
            get => _videoSource;
            set { _videoSource = value; OnPropertyChanged(); }
        }
    }

    public class GroupCallVM : ViewModelBase
    {
        private readonly CallSignalRService _signalR;
        private readonly WebRTCManager _webrtc;
        private readonly string _roomId;

        public ObservableCollection<RemotePeerVM> RemotePeers { get; } = new();

        public ICommand LeaveCallCommand { get; }
        public ICommand ShareScreenCommand { get; }
        public ICommand ToggleCameraCommand { get; }
        public ICommand ToggleMicCommand { get; }

        private bool _isCameraOn = true;
        private bool _isMicOn = true;

        public GroupCallVM(string roomId)
        {
            _roomId = roomId;
            _signalR = new CallSignalRService();
            _webrtc = new WebRTCManager();

            LeaveCallCommand = new RelayCommand(async _ => await LeaveCallAsync());
            ShareScreenCommand = new RelayCommand(_ => ShareScreen());
            ToggleCameraCommand = new RelayCommand(_ => ToggleCamera());
            ToggleMicCommand = new RelayCommand(_ => ToggleMic());

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await _webrtc.InitializeLocalTracksAsync();

            _webrtc.OnRemoteTrackAdded = (track, mediaType, fromId) =>
            {
                if (mediaType != "camera") return;

                track.Argb32VideoFrameReady += frame =>
                {
                    var bitmap = VideoFrameConverter.ConvertToBitmap(frame);

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        var existing = RemotePeers.FirstOrDefault(p => p.ConnectionId == fromId);
                        if (existing == null)
                        {
                            existing = new RemotePeerVM { ConnectionId = fromId };
                            RemotePeers.Add(existing);
                        }

                        existing.VideoSource = bitmap; // update frame
                    });
                };
            };


            _webrtc.IceCandidateCallback = async (toId, candidate, mid, index) =>
                await _signalR.SendIceCandidateAsync(toId, candidate, mid, index);

            _webrtc.SdpReadyToSendCallback = async (toId, sdp, type) =>
            {
                if (type == "offer")
                    await _signalR.SendOfferAsync(toId, sdp, "camera");
                else
                    await _signalR.SendAnswerAsync(toId, sdp, "camera");
            };

            await _signalR.InitializeAsync("https://localhost:7152/callhub");
            await _signalR.JoinCallRoomAsync(_roomId);

            _signalR.OnUsersInCallReceived += async list =>
            {
                foreach (var userId in list)
                {
                    await _webrtc.CreatePeerForUserAsync(userId);
                    _webrtc.CreateOffer(userId);
                }
            };

            _signalR.OnUserJoinedCall += async connId =>
            {
                await _webrtc.CreatePeerForUserAsync(connId);
                _webrtc.CreateOffer(connId);
            };

            _signalR.OnOfferReceived += async (fromId, sdp, media) =>
            {
                await _webrtc.CreatePeerForUserAsync(fromId);
                await _webrtc.SetRemoteSdpAsync(fromId, sdp, true);
                _webrtc.CreateAnswer(fromId);
            };

            _signalR.OnAnswerReceived += async (fromId, sdp, media) =>
                await _webrtc.SetRemoteSdpAsync(fromId, sdp, false);

            _signalR.OnIceCandidateReceived += (fromId, candidate, mid, index) =>
            {
                var ice = new IceCandidate
                {
                    Content = candidate,
                    SdpMid = mid,
                    SdpMlineIndex = index
                };
                _webrtc.AddIceCandidate(fromId, ice);
            };

            _signalR.OnUserLeftCall += connId =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var peer = RemotePeers.FirstOrDefault(p => p.ConnectionId == connId);
                    if (peer != null)
                        RemotePeers.Remove(peer);
                });

                _webrtc.RemovePeer(connId);
            };

        }

        private void ShareScreen()
        {
            _webrtc.EnableScreenTrack(true);
        }

        private void ToggleCamera()
        {
            _isCameraOn = !_isCameraOn;
            _webrtc.ToggleCamera(_isCameraOn);
        }

        private void ToggleMic()
        {
            _isMicOn = !_isMicOn;
            _webrtc.ToggleMic(_isMicOn);
        }

        private async Task LeaveCallAsync()
        {
            await _signalR.DisconnectAsync();
            RemotePeers.Clear();
        }
    }
}