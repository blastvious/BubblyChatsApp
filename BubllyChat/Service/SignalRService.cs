using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubllyChat.Service
{
    public class SignalRService
    {
        private HubConnection _hubConnection;
        private const string HubUrl = "http://localhost:5136/chathub";

        // Events để ViewModel có thể subscribe - cập nhật để nhận avatar
        public event Action<string, string, DateTime, string> OnMessageReceived;
        public event Action<string> OnUserJoined;
        public event Action<string> OnUserLeft;
        public event Action<string> OnConnected;
        public event Action<string> OnDisconnected;

        public async Task InitializeAsync()
        {
            try
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(HubUrl)
                    .WithAutomaticReconnect() // Tự động kết nối lại
                    .Build();

                // Đăng ký các sự kiện từ server - cập nhật để nhận avatar
                _hubConnection.On<string, string, DateTime, string>("ReceiveMessage", (user, message, time, avatarUrl) =>
                {
                    OnMessageReceived?.Invoke(user, message, time, avatarUrl);
                });


                _hubConnection.On<string>("UserJoined", (user) =>
                {
                    OnUserJoined?.Invoke(user);
                });

                _hubConnection.On<string>("UserLeft", (user) =>
                {
                    OnUserLeft?.Invoke(user);
                });

                // Xử lý kết nối/ngắt kết nối
                _hubConnection.Closed += async (error) =>
                {
                    OnDisconnected?.Invoke(error?.Message ?? "Connection closed");
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await ConnectAsync();
                };

                _hubConnection.Reconnected += (connectionId) =>
                {
                    OnConnected?.Invoke($"Reconnected with ID: {connectionId}");
                    return Task.CompletedTask;
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize SignalR: {ex.Message}");
            }
        }

        public async Task ConnectAsync()
        {
            try
            {
                if (_hubConnection?.State != HubConnectionState.Connected)
                {
                    await _hubConnection.StartAsync();
                    OnConnected?.Invoke($"Connected with ID: {_hubConnection.ConnectionId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to connect to SignalR: {ex.Message}");
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                if (_hubConnection?.State == HubConnectionState.Connected)
                {
                    await _hubConnection.StopAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to disconnect from SignalR: {ex.Message}");
            }
        }

        // Phương thức gửi tin nhắn đến room với avatar
        public async Task SendMessageToRoomAsync(string roomId, string user, string message, string avatarUrl)
        {
            try
            {
                if (_hubConnection?.State == HubConnectionState.Connected)
                {
                    await _hubConnection.InvokeAsync("SendMessageToRoom", roomId, user, message, avatarUrl);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send message: {ex.Message}");
            }
        }

        // Phương thức gửi tin nhắn riêng tư với avatar
        public async Task SendPrivateMessageAsync(string toUserId, string fromUser, string message, string avatarUrl)
        {
            try
            {
                if (_hubConnection?.State == HubConnectionState.Connected)
                {
                    await _hubConnection.InvokeAsync("SendPrivateMessage", toUserId, fromUser, message, avatarUrl);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send private message: {ex.Message}");
            }
        }

        // Phương thức tham gia room
        public async Task JoinRoomAsync(string roomId, string userId)
        {
            try
            {
                if (_hubConnection?.State == HubConnectionState.Connected)
                {
                    await _hubConnection.InvokeAsync("JoinRoom", roomId, userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to join room: {ex.Message}");
            }
        }

        // Phương thức rời room
        public async Task LeaveRoomAsync(string roomId, string userId)
        {
            try
            {
                if (_hubConnection?.State == HubConnectionState.Connected)
                {
                    await _hubConnection.InvokeAsync("LeaveRoom", roomId, userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to leave room: {ex.Message}");
            }
        }

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public void Dispose()
        {
            _hubConnection?.DisposeAsync();
        }
    }
}
