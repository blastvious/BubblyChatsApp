using BubllyChat.Core;
using BubllyChat.MVVM.Models;
using BubllyChat.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace BubllyChat.MVVM.ViewModel
{
    public class FriendandRoomVM : ViewModelBase
    {
        private string _searchText;
        private string _colorStatusMessage = "#D75960";
        private string _statusMessage;
        private Stream _selectedAvatarStream;
        private string _selectedFileExtension;
        private string _avatarroomUrl;
        private readonly FirebaseRTDB _firebaseRTDB = new FirebaseRTDB();
        private readonly FirebaseStorageService _storageService = new FirebaseStorageService();
        // Update the type of _currentUser to match the expected type in FirebaseRTDB methods
        private Users _currentUser; // Change from BubllyChat.MVVM.Models.Users to Users
        public string AvatarUrl
        {
            get { return _avatarroomUrl; }
            set
            {
                _avatarroomUrl = value;
                OnPropertyChanged(nameof(AvatarUrl));
            }
        }
        public ICommand AddFriend { get; set; }
        public ICommand JoinRoom { get; set; }
        public ICommand CreateRoom { get; set; }
        public ICommand UploadAvatar { get; set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public string ColorStatusMessage
        {
            get { return _colorStatusMessage; }
            set
            {
                _colorStatusMessage = value;
                OnPropertyChanged(nameof(ColorStatusMessage));
            }
        }
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public object DialogResult { get; private set; }

        public FriendandRoomVM()
        {
            UploadAvatar = new RelayCommand(ExecuteUploadAvt);
            AddFriend = new RelayCommand(AddFriendExecute, CanExecuteAddFriend);
            JoinRoom = new RelayCommand(JoinRoomExecute);
            CreateRoom = new RelayCommand(CreateRoomExecute);
            AvatarUrl = "/Images/room.png";
            _currentUser = CurrentUserService.CurrentUser;
        }

        //private void ExcuteUploadAvt(object obj)
        //{
        //    var openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png",
        //    };
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        _selectedAvatarStream?.Dispose(); // Dispose the previous stream if it exists
        //        //Get the selected file stream
        //        _selectedAvatarStream = File.OpenRead(openFileDialog.FileName);

        //        _selectedFileExtension = Path.GetExtension(openFileDialog.FileName);

        //        AvatarUrl = openFileDialog.FileName;
        //        OnPropertyChanged(nameof(AvatarUrl));


        //    }

        //}
        // Đây là namespace đúng cho WPF OpenFileDialog

        private void ExecuteUploadAvt(object obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png",
            };

            bool? result = openFileDialog.ShowDialog(); // Nullable<bool>

            if (result == true)
            {
                _selectedAvatarStream?.Dispose();
                _selectedAvatarStream = File.OpenRead(openFileDialog.FileName);
                _selectedFileExtension = Path.GetExtension(openFileDialog.FileName);

                AvatarUrl = openFileDialog.FileName;
                OnPropertyChanged(nameof(AvatarUrl));
            }
        }




        private async void CreateRoomExecute(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    StatusMessage = "Please enter a room name.";
                    ColorStatusMessage = "#D75960";
                    return;
                }
                //Kiem tra ten phong da ton tai chua
                var existingRoom = await _firebaseRTDB.FindRoomByNameAsync(SearchText.Trim());
                if (existingRoom != null)
                {
                    StatusMessage = "Room name already exists. Please choose a different name.";
                    ColorStatusMessage = "#D75960";
                    return;
                }


                string avatarUrl;
                string filename = null;

                // Xử lý avatar
                if (_selectedAvatarStream != null)
                {
                    filename = Guid.NewGuid().ToString() + _selectedFileExtension;
                    _selectedAvatarStream.Seek(0, SeekOrigin.Begin);
                    await _storageService.UpLoadAvatarAsync(filename, _selectedAvatarStream);
                    avatarUrl = filename;  // Chỉ lưu tên file, KHÔNG lưu URL
                    _selectedAvatarStream.Dispose();
                    _selectedAvatarStream = null;
                }
                else
                {
                    // Sử dụng avatar mặc định
                    avatarUrl = null;
                }

                // Tạo đối tượng phòng mới
                var newRoom = new RoomModel
                {
                    RoomId = Guid.NewGuid().ToString(),
                    DisplayName = SearchText.Trim(),
                    ImageSource = avatarUrl,
                    CreatedBy = CurrentUserService.CurrentUser.DisplayName,
                    CreatedAt = DateTime.Now,
                    Members = new List<string> { _currentUser.Id }
                };

                // Lưu phòng lên Realtime Database
                await _firebaseRTDB.SaveRoomAsync(newRoom);

                // Cập nhật người dùng (thêm room vào danh sách)
                if (_currentUser.Rooms == null)
                {
                    _currentUser.Rooms = new List<string>();
                }
                _currentUser.Rooms.Add(newRoom.RoomId);
                await _firebaseRTDB.UpdateUserAsync(_currentUser);

                // Reset giao diện
                SearchText = string.Empty;
                AvatarUrl = "/Images/room.png";

                // Hiển thị thông báo thành công
                StatusMessage = "Room created successfully!";
                ColorStatusMessage = "#4CAF50";


            }
            catch (Exception ex)
            {
                // HIển thị thông báo khi lỗi   
                StatusMessage = $"Error creating room: {ex.Message}";
                ColorStatusMessage = "#D75960";
                Console.WriteLine($"CreateRoom Error: {ex}");
            }
        }



        private async void JoinRoomExecute(object obj)
        {
            try
            {
                StatusMessage = "";
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    StatusMessage = "Please enter a room ID.";
                    ColorStatusMessage = "#D75960";
                    return;
                }
                // Tìm kiếm phòng theo ten

                var room = await _firebaseRTDB.FindRoomByNameAsync(SearchText.Trim());
                if (room == null)
                {
                    StatusMessage = "Room not found.";
                    ColorStatusMessage = "#D75960";
                    return;
                }
                // Kiểm tra xem người dùng đã tham gia phòng chưa
                if (room.Members.Contains(_currentUser.Id))
                {
                    StatusMessage = "You are already a member of this room.";
                    ColorStatusMessage = "#D75960";
                    return;
                }

                room.Members.Add(_currentUser.Id);
                await _firebaseRTDB.SaveRoomAsync(room);
                // Cập nhật danh sách phòng của người dùng
                if (_currentUser.Rooms == null)
                {
                    _currentUser.Rooms = new List<string>();
                }
                if (!_currentUser.Rooms.Contains(room.RoomId))
                {
                    _currentUser.Rooms.Add(room.RoomId);
                    await _firebaseRTDB.UpdateUserAsync(_currentUser);
                }
                await _firebaseRTDB.UpdateUserAsync(_currentUser);
                CurrentUserService.CurrentUser = _currentUser;

                StatusMessage = "Joined room successfully!";
                ColorStatusMessage = "#4CAF50";
                // Reset giao diện
                SearchText = string.Empty;



            }
            catch (Exception ex)
            {
                StatusMessage = $"Error joining room: {ex.Message}";
                ColorStatusMessage = "#D75960";
                Console.WriteLine($"JoinRoom Error: {ex}");
            }
        }
        // Trong FriendandRoomVM.cs
        private async Task LeaveRoomAsync(string roomId)
        {
            try
            {
                var room = await _firebaseRTDB.GetRoomAsync(roomId);
                // Kiểm tra xem phòng có tồn tại không và người dùng có trong phòng không
                if (room != null && room.Members.Contains(_currentUser.Id))
                {
                    room.Members.Remove(_currentUser.Id);
                    await _firebaseRTDB.SaveRoomAsync(room);
                }
                // Kiểm tra và cập nhật danh sách phòng của người dùng
                if (_currentUser.Rooms != null && _currentUser.Rooms.Contains(roomId))
                {
                    _currentUser.Rooms.Remove(roomId);
                    await _firebaseRTDB.UpdateUserAsync(_currentUser);
                    CurrentUserService.CurrentUser = _currentUser;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error leaving room: {ex.Message}");
            }
        }
        private bool CanExecuteAddFriend(object arg)
        {
            throw new NotImplementedException();
        }

        private void AddFriendExecute(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
