using BubllyChat.Core;
using BubllyChat.MVVM.Models;
using BubllyChat.MVVM.View;
using BubllyChat.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BubllyChat.MVVM.ViewModel
{
    internal enum Phase { Welcome, Waiting, Chatting }
    public class ChattingVM : ViewModelBase
    {
        public ObservableCollection<ContactModel> _Contacts { get; set; }
        public ObservableCollection<RoomModel> _Rooms { get; set; }
        private Users _currentUser;
        private FirebaseStorageService _storageService;
        private FirebaseRTDB _firebaseRTDB;

        private SignalRService _signalRService;
        private string _avatarCurrentUser;
        public string AvatarCurrentUser
        {
            get => _avatarCurrentUser;
            set { _avatarCurrentUser = value; OnPropertyChanged(); }
        }

        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(IsRoomSelected));
                OnPropertyChanged(nameof(IsContactSelected));
                OnPropertyChanged(nameof(SelectedRoom));
                OnPropertyChanged(nameof(SelectedContact));
                _ = HandleRoomSelectionAsync();
                UpdateChatView();
            }
        }

        public bool IsRoomSelected => SelectedItem is RoomModel;
        public bool IsContactSelected => SelectedItem is ContactModel;
        public RoomModel SelectedRoom => SelectedItem as RoomModel;
        public ContactModel SelectedContact => SelectedItem as ContactModel;

        public ObservableCollection<object> ChatItems { get; } = new ObservableCollection<object>();

        private ObservableCollection<MessageModel> _currentMessages;
        public ObservableCollection<MessageModel> CurrentMessages
        {
            get => _currentMessages;
            set
            {
                _currentMessages = value;
                OnPropertyChanged();
            }
        }

        public Users CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(); }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set { _isConnected = value; OnPropertyChanged(); }
        }

        private string _connectionStatus = "Disconnected";
        public string ConnectionStatus
        {
            get => _connectionStatus;
            set { _connectionStatus = value; OnPropertyChanged(); }
        }

        public RelayCommand SendCommand { get; set; }
        public RelayCommand AddFriendCommand { get; }
        public RelayCommand GroupCallCommand { get; set; }

        public ChattingVM()
        {
            _signalRService = new SignalRService();
            AvatarCurrentUser = "/Images/user.png";
            _storageService = new FirebaseStorageService();
            _firebaseRTDB = new FirebaseRTDB();

            _Contacts = new ObservableCollection<ContactModel>();
            _Rooms = new ObservableCollection<RoomModel>();

            SetupSignalREvents();
            SetupCommands();
            AddSampleRoom();
            AddFriendCommand = new RelayCommand(o =>
            {
                var addFriendV = new AddFriend();
                addFriendV.ShowDialog();
            });
        }

        private void SetupSignalREvents()
        {
            // Đăng ký events từ SignalR - cập nhật để nhận avatar
            _signalRService.OnMessageReceived += OnMessageReceived;
            //_signalRService.OnUserJoined += OnUserJoined;
            _signalRService.OnUserLeft += OnUserLeft;
            _signalRService.OnConnected += OnConnected;
            _signalRService.OnDisconnected += OnDisconnected;
        }

        private void SetupCommands()
        {
            SendCommand = new RelayCommand(async o =>
            {
                await SendMessageAsync();
            });

            GroupCallCommand = new RelayCommand(o =>
            {
                if (IsRoomSelected && SelectedRoom != null)
                {
                    var groupCallWindow = new GroupCallView(SelectedRoom.RoomId);
                    groupCallWindow.Show();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một phòng để bắt đầu gọi nhóm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });

        }

        private async Task SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(Message) || CurrentUser == null) return;

            try
            {
                var newMessage = new MessageModel
                {
                    DisplayName = CurrentUser.DisplayName,
                    Message = Message,
                    Time = DateTime.Now,
                    IsNativeOrigin = true,
                    ImageSource = AvatarCurrentUser,
                    FirstMessage = false,
                };

                // Gửi tin nhắn qua SignalR
                if (IsRoomSelected && SelectedRoom != null)
                {
                    // Gửi tin nhắn đến room qua SignalR với avatar
                    await _signalRService.SendMessageToRoomAsync(
                        SelectedRoom.RoomId,
                        CurrentUser.DisplayName,
                        Message,
                        AvatarCurrentUser); // Thêm avatar vào đây

                    // Thêm tin nhắn vào UI (sẽ được duplicate khi nhận từ SignalR, cần xử lý)
                    // SelectedRoom.Messages.Add(newMessage);
                    // CurrentMessages = SelectedRoom.Messages;
                }
                else if (IsContactSelected && SelectedContact != null)
                {
                    // Xử lý tin nhắn riêng tư với avatar
                    await _signalRService.SendPrivateMessageAsync(
                        SelectedContact.DisplayName,
                        CurrentUser.DisplayName,
                        Message,
                        AvatarCurrentUser); // Thêm avatar vào đây

                    SelectedContact.Messages.Add(newMessage);
                    CurrentMessages = SelectedContact.Messages;
                }

                Message = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi gửi tin nhắn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task HandleRoomSelectionAsync()
        {
            if (IsRoomSelected && SelectedRoom != null && CurrentUser != null)
            {
                try
                {
                    // Tham gia room qua SignalR
                    await _signalRService.JoinRoomAsync(SelectedRoom.RoomId, CurrentUser.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi tham gia room: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // SignalR Event Handlers - cập nhật để nhận avatar
        private void OnMessageReceived(string user, string message, DateTime time, string avatarUrl)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (IsRoomSelected && SelectedRoom != null)
                {
                    var newMessage = new MessageModel
                    {
                        DisplayName = user,
                        Message = message,
                        Time = time,
                        IsNativeOrigin = user == CurrentUser?.DisplayName,
                        ImageSource = avatarUrl ?? "/Images/user.png", // Sử dụng avatar từ server
                        FirstMessage = false
                    };

                    SelectedRoom.Messages.Add(newMessage);
                    CurrentMessages = SelectedRoom.Messages;
                }
            });
        }

        //private void OnUserJoined(string user)
        //{
        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        // Có thể hiển thị thông báo user joined
        //        if (IsRoomSelected && SelectedRoom != null)
        //        {
        //            var systemMessage = new MessageModel
        //            {
        //                DisplayName = "System",
        //                Message = $"{user} đã tham gia phòng",
        //                Time = DateTime.Now,
        //                IsNativeOrigin = false,
        //                ImageSource = null,
        //                FirstMessage = false
        //            };

        //            SelectedRoom.Messages.Add(systemMessage);
        //            CurrentMessages = SelectedRoom.Messages;
        //        }
        //    });
        //}

        private void OnUserLeft(string user)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Có thể hiển thị thông báo user left
                if (IsRoomSelected && SelectedRoom != null)
                {
                    var systemMessage = new MessageModel
                    {
                        DisplayName = "System",
                        Message = $"{user} đã rời phòng",
                        Time = DateTime.Now,
                        IsNativeOrigin = false,
                        ImageSource = "/Images/system.png",
                        FirstMessage = false
                    };

                    SelectedRoom.Messages.Add(systemMessage);
                    CurrentMessages = SelectedRoom.Messages;
                }
            });
        }

        private void OnConnected(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IsConnected = true;
                ConnectionStatus = "Connected";
            });
        }

        private void OnDisconnected(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IsConnected = false;
                ConnectionStatus = $"Disconnected: {message}";
            });
        }

        public async Task ConnectAsync()
        {
            try
            {
                CurrentUser = CurrentUserService.CurrentUser;

                await _signalRService.InitializeAsync();
                await _signalRService.ConnectAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối SignalR: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateChatView()
        {
            if (IsContactSelected)
            {
                CurrentMessages = SelectedContact.Messages;
                if (!CurrentMessages.Any())
                    LoadMessagesForContact(SelectedContact);
            }
            else if (IsRoomSelected)
            {
                CurrentMessages = SelectedRoom.Messages;
                if (!CurrentMessages.Any())
                    LoadMessagesForRoom(SelectedRoom);
            }
            else
            {
                CurrentMessages = new ObservableCollection<MessageModel>();
            }
        }

        private void LoadMessagesForContact(ContactModel contact)
        {
            // Tải tin nhắn cho contact
        }

        private void LoadMessagesForRoom(RoomModel room)
        {
            // Tải tin nhắn cho room
        }

        private async Task SetRoomAvatarAsync(RoomModel room)
        {
            if (!string.IsNullOrEmpty(room.ImageSource))
            {
                var avatarUrl = await _storageService.GetAvatarUrlAsync(room.ImageSource);
                room.ImageSource = !string.IsNullOrEmpty(avatarUrl) ? avatarUrl : "/Images/room.png";
            }
            else
            {
                room.ImageSource = "/Images/room.png";
            }
        }

        private void AddSampleRoom()
        {
            var room = new RoomModel
            {
                DisplayName = $"Project Room",
                ImageSource = "/Images/1.jpg",
                RoomId = $"room001",
                CreatedBy = "Admin",
                CreatedAt = DateTime.Now,
                Members = new List<string> { "user1", "user2", "user3" }
            };

            room.Messages.Add(new MessageModel
            {
                DisplayName = "System",
                Message = "Chào mừng đến với phòng chat!",
                Time = DateTime.Now.AddHours(-1),
                IsNativeOrigin = false,
                ImageSource = "/Images/system.png"
            });

            _Rooms.Add(room);
            ChatItems.Add(room);
        }

        public async Task InitAsync()
        {
            CurrentUser = CurrentUserService.CurrentUser;
            string basicAvatar = "/Images/user.png";
            AvatarCurrentUser = basicAvatar;

            if (_currentUser == null) return;

            if (!string.IsNullOrEmpty(_currentUser.Avatar))
            {
                var url = await _storageService.GetAvatarUrlAsync(_currentUser.Avatar);
                AvatarCurrentUser = !string.IsNullOrEmpty(url) ? url : basicAvatar;
            }

            var userRoom = _currentUser.Rooms;

            if (userRoom != null && userRoom.Any())
            {
                foreach (var roomId in userRoom)
                {
                    var room = await _firebaseRTDB.GetRoomAsync(roomId);
                    if (room != null)
                    {
                        await SetRoomAvatarAsync(room);
                        _Rooms.Add(room);
                        ChatItems.Add(room);
                    }
                }
            }
        }

        // Dispose resources
        public void Dispose()
        {
            _signalRService?.Dispose();
        }
    }
}
