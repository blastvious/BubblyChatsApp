
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
namespace BubllyChatServer.Hubs
{
    public class ChatHub : Hub
    {
        // Dictionary để track user trong room nào
        private static readonly ConcurrentDictionary<string, string> UserConnections = new();
        private static readonly ConcurrentDictionary<string, List<string>> RoomUsers = new();

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Xử lý khi user disconnect
            if (UserConnections.TryGetValue(Context.ConnectionId, out var roomId))
            {
                await LeaveRoom(roomId, Context.ConnectionId);
            }

            UserConnections.TryRemove(Context.ConnectionId, out _);
            await base.OnDisconnectedAsync(exception);
        }

        // Tham gia room
        public async Task JoinRoom(string roomId, string userId)
        {
            try
            {
                // Thêm connection vào group
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

                // Track user trong room
                UserConnections[Context.ConnectionId] = roomId;

                if (!RoomUsers.ContainsKey(roomId))
                {
                    RoomUsers[roomId] = new List<string>();
                }

                if (!RoomUsers[roomId].Contains(userId))
                {
                    RoomUsers[roomId].Add(userId);
                }

                // Thông báo cho các user khác trong room
                await Clients.Group(roomId).SendAsync("UserJoined", userId);

                // Gửi danh sách user hiện tại cho user mới join
                await Clients.Caller.SendAsync("RoomUsers", RoomUsers[roomId]);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", $"Failed to join room: {ex.Message}");
            }
        }

        // Rời room
        public async Task LeaveRoom(string roomId, string userId)
        {
            try
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

                if (RoomUsers.ContainsKey(roomId))
                {
                    RoomUsers[roomId].Remove(userId);
                    if (RoomUsers[roomId].Count == 0)
                    {
                        RoomUsers.TryRemove(roomId, out _);
                    }
                }

                UserConnections.TryRemove(Context.ConnectionId, out _);

                // Thông báo cho các user khác
                await Clients.Group(roomId).SendAsync("UserLeft", userId);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", $"Failed to leave room: {ex.Message}");
            }
        }

        // Gửi tin nhắn đến room với avatar
        public async Task SendMessageToRoom(string roomId, string user, string message, string avatarUrl)
        {
            try
            {
                var timestamp = DateTime.Now;

                // Gửi tin nhắn đến tất cả user trong room bao gồm avatar
                await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message, timestamp, avatarUrl);

                // Có thể lưu tin nhắn vào database ở đây
                // await SaveMessageToDatabase(roomId, user, message, timestamp, avatarUrl);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", $"Failed to send message: {ex.Message}");
            }
        }

        // Gửi tin nhắn riêng tư với avatar
        public async Task SendPrivateMessage(string toUserId, string fromUser, string message, string avatarUrl)
        {
            try
            {
                var timestamp = DateTime.Now;

                // Tìm connection của user đích
                var toConnection = UserConnections.FirstOrDefault(x => x.Value == toUserId).Key;

                if (!string.IsNullOrEmpty(toConnection))
                {
                    await Clients.Client(toConnection).SendAsync("ReceivePrivateMessage", fromUser, message, timestamp, avatarUrl);
                    await Clients.Caller.SendAsync("MessageSent", toUserId, message, timestamp, avatarUrl);
                }
                else
                {
                    await Clients.Caller.SendAsync("Error", "User not found or offline");
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", $"Failed to send private message: {ex.Message}");
            }
        }

        // Typing indicator
        public async Task StartTyping(string roomId, string userId)
        {
            await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("UserStartedTyping", userId);
        }

        public async Task StopTyping(string roomId, string userId)
        {
            await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("UserStoppedTyping", userId);
        }
    }
}
