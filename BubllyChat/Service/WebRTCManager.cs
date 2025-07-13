using Microsoft.MixedReality.WebRTC;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media;

namespace BubllyChat.Service
{
    public class WebRTCManager
    {
        private readonly ConcurrentDictionary<string, PeerConnection> _peerConnections = new();

        public LocalVideoTrack CameraTrack { get; private set; }
        public ExternalVideoTrackSource ScreenSource { get; private set; }
        public LocalVideoTrack ScreenTrack { get; private set; }
        private LocalAudioTrack _audioTrack;

        private bool _isCameraEnabled = true;
        private bool _isMicEnabled = true;
        private bool _isScreenSharing = true;

        public Action<RemoteVideoTrack, string, string> OnRemoteTrackAdded;
        public Action<string, string, string, int> IceCandidateCallback;
        public Action<string, string, string> SdpReadyToSendCallback;

        public async Task InitializeLocalTracksAsync()
        {
            try
            {
                var videoSource = await DeviceVideoTrackSource.CreateAsync();
                CameraTrack = LocalVideoTrack.CreateFromSource(videoSource, new LocalVideoTrackInitConfig { trackName = "camera_track" });

                ScreenSource = ExternalVideoTrackSource.CreateFromArgb32Callback((in FrameRequest request) => { });
                ScreenTrack = LocalVideoTrack.CreateFromSource(ScreenSource, new LocalVideoTrackInitConfig { trackName = "screen_track" });

                var audioSource = await DeviceAudioTrackSource.CreateAsync();
                _audioTrack = LocalAudioTrack.CreateFromSource(audioSource, new LocalAudioTrackInitConfig { trackName = "mic_track" });

                StartCapturingScreen();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[WebRTC Init ERROR] {ex}");
                MessageBox.Show($"Lỗi khởi tạo thiết bị: {ex.Message}", "WebRTC", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public async Task<PeerConnection> CreatePeerForUserAsync(string connectionId)
        {
            var peer = new PeerConnection();
            await peer.InitializeAsync();

            peer.IceCandidateReadytoSend += candidate =>
            {
                IceCandidateCallback?.Invoke(connectionId, candidate.Content, candidate.SdpMid, candidate.SdpMlineIndex);
            };
            peer.LocalSdpReadytoSend += sdp =>
            {
                SdpReadyToSendCallback?.Invoke(connectionId, sdp.Content, sdp.Type.ToString().ToLower());
            };
            peer.VideoTrackAdded += track =>
            {
                OnRemoteTrackAdded?.Invoke(track, "camera", connectionId);
            };

            var camTransceiver = peer.AddTransceiver(MediaKind.Video);
            camTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;
            camTransceiver.LocalVideoTrack = _isCameraEnabled ? CameraTrack : null;

            var screenTransceiver = peer.AddTransceiver(MediaKind.Video);
            screenTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;
            screenTransceiver.LocalVideoTrack = _isScreenSharing ? ScreenTrack : null;

            var audioTransceiver = peer.AddTransceiver(MediaKind.Audio);
            audioTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;
            audioTransceiver.LocalAudioTrack = _isMicEnabled ? _audioTrack : null;

            _peerConnections[connectionId] = peer;
            return peer;
        }

        public void CreateOffer(string connectionId)
        {
            if (_peerConnections.TryGetValue(connectionId, out var peer))
                peer.CreateOffer();
        }

        public void CreateAnswer(string connectionId)
        {
            if (_peerConnections.TryGetValue(connectionId, out var peer))
                peer.CreateAnswer();
        }

        public async Task SetRemoteSdpAsync(string connectionId, string sdp, bool isOffer)
        {
            if (_peerConnections.TryGetValue(connectionId, out var peer))
            {
                var desc = new SdpMessage
                {
                    Type = isOffer ? SdpMessageType.Offer : SdpMessageType.Answer,
                    Content = sdp
                };
                await peer.SetRemoteDescriptionAsync(desc);
            }
        }

        public void AddIceCandidate(string connectionId, IceCandidate candidate)
        {
            if (_peerConnections.TryGetValue(connectionId, out var peer))
            {
                peer.AddIceCandidate(candidate);
            }
        }

        public void RemovePeer(string connectionId)
        {
            if (_peerConnections.TryRemove(connectionId, out var peer))
            {
                peer.Close();
                peer.Dispose();
            }
        }

        private void StartCapturingScreen()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (_isScreenSharing)
                        CaptureScreenFrame();
                    await Task.Delay(100);
                }
            });
        }

        private void CaptureScreenFrame()
        {
            using var bmp = new Bitmap(1280, 720);
            using var g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, bmp.Size);

            var bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            unsafe
            {
                var frame = new Argb32VideoFrame
                {
                    width = (uint)bmp.Width,
                    height = (uint)bmp.Height,
                    stride = Math.Abs(bmpData.Stride),
                    data = bmpData.Scan0
                };

                ScreenSource.CompleteFrameRequest(0, DateTimeOffset.Now.ToUnixTimeMilliseconds(), frame);
            }

            bmp.UnlockBits(bmpData);
        }

        public void ToggleCamera(bool enable)
        {
            _isCameraEnabled = enable;
            foreach (var peer in _peerConnections.Values)
            {
                var transceiver = peer.Transceivers.FirstOrDefault(t => t.MediaKind == MediaKind.Video && t.LocalVideoTrack == CameraTrack);
                if (transceiver != null)
                    transceiver.LocalVideoTrack = enable ? CameraTrack : null;
            }
        }

        public void ToggleMic(bool enable)
        {
            _isMicEnabled = enable;
            foreach (var peer in _peerConnections.Values)
            {
                var transceiver = peer.Transceivers.FirstOrDefault(t => t.MediaKind == MediaKind.Audio);
                if (transceiver != null)
                    transceiver.LocalAudioTrack = enable ? _audioTrack : null;
            }
        }

        public void EnableScreenTrack(bool enable)
        {
            _isScreenSharing = enable;
            foreach (var peer in _peerConnections.Values)
            {
                var transceiver = peer.Transceivers.FirstOrDefault(t => t.MediaKind == MediaKind.Video && t.LocalVideoTrack == ScreenTrack);
                if (transceiver != null)
                    transceiver.LocalVideoTrack = enable ? ScreenTrack : null;
            }
        }
    }
}