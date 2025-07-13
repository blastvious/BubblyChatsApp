using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubllyChat.Service
{
    public class CallSignalRService
    {
        private HubConnection _hubConnection;

        public event Action<string, string, string> OnOfferReceived;
        public event Action<string, string, string> OnAnswerReceived;
        public event Action<string, string, string, int> OnIceCandidateReceived;

        public event Action<List<string>> OnUsersInCallReceived;
        public event Action<string> OnUserJoinedCall;
        public event Action<string> OnUserLeftCall;

        public async Task InitializeAsync(string hubUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl) // Không truyền userId nữa
                .WithAutomaticReconnect()
                .Build();

            // Setup signal handlers
            _hubConnection.On<string, string, string>("ReceiveOffer", (fromConnectionId, sdp, mediaType) =>
            {
                OnOfferReceived?.Invoke(fromConnectionId, sdp, mediaType);
            });

            _hubConnection.On<string, string, string>("ReceiveAnswer", (fromConnectionId, sdp, mediaType) =>
            {
                OnAnswerReceived?.Invoke(fromConnectionId, sdp, mediaType);
            });

            _hubConnection.On<string, string, string, int>("ReceiveIceCandidate", (fromConnectionId, candidate, sdpMid, sdpMlineIndex) =>
            {
                OnIceCandidateReceived?.Invoke(fromConnectionId, candidate, sdpMid, sdpMlineIndex);
            });

            _hubConnection.On<List<string>>("UsersInCall", (connectionIds) =>
            {
                OnUsersInCallReceived?.Invoke(connectionIds);
            });

            _hubConnection.On<string>("UserJoinedCall", (connectionId) =>
            {
                OnUserJoinedCall?.Invoke(connectionId);
            });

            _hubConnection.On<string>("UserLeftCall", (connectionId) =>
            {
                OnUserLeftCall?.Invoke(connectionId);
            });

            await _hubConnection.StartAsync();
        }

        public async Task JoinCallRoomAsync(string roomId)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.InvokeAsync("JoinCallRoom", roomId);
            }
        }

        public async Task SendOfferAsync(string toConnectionId, string sdp, string mediaType)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.InvokeAsync("SendOffer", toConnectionId, sdp, mediaType);
            }
        }

        public async Task SendAnswerAsync(string toConnectionId, string sdp, string mediaType)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.InvokeAsync("SendAnswer", toConnectionId, sdp, mediaType);
            }
        }

        public async Task SendIceCandidateAsync(string toConnectionId, string candidate, string sdpMid, int sdpMlineIndex)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.InvokeAsync("SendIceCandidate", toConnectionId, candidate, sdpMid, sdpMlineIndex);
            }
        }

        public async Task DisconnectAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
            }
        }

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;
    }
}
