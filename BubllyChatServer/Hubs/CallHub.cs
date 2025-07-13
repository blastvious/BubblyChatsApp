using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
namespace BubllyChatServer.Hubs
{
    public class CallHub : Hub
    {
        private static readonly ConcurrentDictionary<string, List<string>> RoomConnections = new();
        // connectionId -> roomId
        private static readonly ConcurrentDictionary<string, string> ConnectionToRoom = new();

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connId = Context.ConnectionId;
            if (ConnectionToRoom.TryRemove(connId, out var roomId))
            {
                if (RoomConnections.TryGetValue(roomId, out var list))
                {
                    list.Remove(connId);
                    if (list.Count == 0)
                    {
                        RoomConnections.TryRemove(roomId, out _);
                    }
                }

                // Thông báo cho các user khác trong phòng rằng user này đã rời khỏi
                await Clients.Group(roomId).SendAsync("UserLeftCall", connId);
                await Groups.RemoveFromGroupAsync(connId, roomId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinCallRoom(string roomId)
        {
            var connId = Context.ConnectionId;

            await Groups.AddToGroupAsync(connId, roomId);

            RoomConnections.AddOrUpdate(roomId,
                _ => new List<string> { connId },
                (_, list) =>
                {
                    lock (list) { list.Add(connId); }
                    return list;
                });

            ConnectionToRoom[connId] = roomId;

            // Gửi danh sách connectionId trong phòng (ngoại trừ bản thân) để start peer connection
            var othersInRoom = RoomConnections[roomId].Where(id => id != connId).ToList();
            await Clients.Caller.SendAsync("UsersInCall", othersInRoom);

            // Thông báo cho người khác là có user mới join
            await Clients.GroupExcept(roomId, connId).SendAsync("UserJoinedCall", connId);
        }

        public async Task SendOffer(string toConnectionId, string sdp, string mediaType)
        {
            await Clients.Client(toConnectionId).SendAsync("ReceiveOffer", Context.ConnectionId, sdp, mediaType);
        }

        public async Task SendAnswer(string toConnectionId, string sdp, string mediaType)
        {
            await Clients.Client(toConnectionId).SendAsync("ReceiveAnswer", Context.ConnectionId, sdp, mediaType);
        }

        public async Task SendIceCandidate(string toConnectionId, string candidate, string sdpMid, int sdpMlineIndex)
        {
            await Clients.Client(toConnectionId).SendAsync("ReceiveIceCandidate", Context.ConnectionId, candidate, sdpMid, sdpMlineIndex);
        }
    }
}
