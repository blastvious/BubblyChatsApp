using Firebase.Database;
using BubllyChat.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace BubllyChat.Service
{
    public class FirebaseRTDB
    {
        private readonly FirebaseClient _firebaseClient;
        private const string DatabaseUrl = "https://bubblychatapp-default-rtdb.firebaseio.com/";
        public FirebaseRTDB()
        {
            _firebaseClient = new FirebaseClient(DatabaseUrl);
        }

        //Luu du lieu user
        public async Task SaveUserAsync(Users user)
        {
            await _firebaseClient
                .Child("users")
                .Child(user.Id)
                .PutAsync(user);
        }

        public async Task UpdateUserAsync(Users user)
        {
            await _firebaseClient
                .Child("users")
                .Child(user.Id)
                .PutAsync(user);
        }

        //Room operations
        public async Task SaveRoomAsync(RoomModel room)
        {
            await _firebaseClient
                .Child("rooms")
                .Child(room.RoomId)
                .PutAsync(room);
        }

        //Lay du lieu user
        public async Task<Users> GetUserAsync(string userId)
        {
            var user = await _firebaseClient
                .Child("users")
                .Child(userId)
                .OnceSingleAsync<Users>();
            return user;
        }

        //Room operations
        public async Task<RoomModel> GetRoomAsync(string roomId)
        {
            var room = await _firebaseClient
                .Child("rooms")
                .Child(roomId)
                .OnceSingleAsync<RoomModel>();
            return room;
        }

        //Finding Rooms
        public async Task<RoomModel> FindRoomByNameAsync(string roomname)
        {
            try
            {
                var allRooms = await _firebaseClient
                    .Child("rooms")
                    .OnceAsync<RoomModel>();
                return allRooms?
                    .Select(x => x.Object)
                    .FirstOrDefault(r => r.DisplayName.Equals(roomname, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding room by name: {ex.Message}");
                return null;
            }
        }

        //Message operations
        public async Task<List<MessageModel>> GetRoomMessagesAsync(string roomId)
        {
            try
            {
                var messages = await _firebaseClient
                    .Child("messages")
                    .Child(roomId)
                    .OnceAsync<MessageModel>();

                return messages?.Select(x => x.Object).OrderBy(m => m.Time).ToList() ?? new List<MessageModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting room messages: {ex.Message}");
                return new List<MessageModel>();
            }
        }

        public async Task SaveMessageAsync(string roomId, MessageModel message)
        {
            try
            {
                await _firebaseClient
                    .Child("messages")
                    .Child(roomId)
                    .PostAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving message: {ex.Message}");
            }
        }

        // Get all rooms for a user
        public async Task<List<RoomModel>> GetUserRoomsAsync(List<string> roomIds)
        {
            try
            {
                var rooms = new List<RoomModel>();

                foreach (var roomId in roomIds)
                {
                    var room = await GetRoomAsync(roomId);
                    if (room != null)
                    {
                        rooms.Add(room);
                    }
                }

                return rooms;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user rooms: {ex.Message}");
                return new List<RoomModel>();
            }
        }

        // Listen for new messages in real-time (optional)
        public void ListenForMessages(string roomId, Action<MessageModel> onMessageReceived)
        {
            try
            {
                _firebaseClient
                    .Child("messages")
                    .Child(roomId)
                    .AsObservable<MessageModel>()
                    .Subscribe(message =>
                    {
                        if (message.Object != null)
                        {
                            onMessageReceived?.Invoke(message.Object);
                        }
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listening for messages: {ex.Message}");
            }
        }

        // Delete a room (optional)
        public async Task DeleteRoomAsync(string roomId)
        {
            try
            {
                await _firebaseClient
                    .Child("rooms")
                    .Child(roomId)
                    .DeleteAsync();

                // Also delete messages
                await _firebaseClient
                    .Child("messages")
                    .Child(roomId)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting room: {ex.Message}");
            }
        }

        // Update room info
        public async Task UpdateRoomAsync(RoomModel room)
        {
            try
            {
                await _firebaseClient
                    .Child("rooms")
                    .Child(room.RoomId)
                    .PutAsync(room);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating room: {ex.Message}");
            }
        }
    }
}
