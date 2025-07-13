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
using System.Windows;
using System.Windows.Input;

namespace BubllyChat.MVVM.ViewModel
{
    public class UpdateProfileVM : ViewModelBase
    {
        private string _username;

        private string _pnumber;

        private DateTime _birthdate = DateTime.Now;
        private Stream _selectedAvatarStream;
        private string _selectedFileExtension;


        private FirebaseRTDB _firebaseRTDB;
        private FirebaseStorageService _storageService;
        private Users _currentUser;

        private string _avatarUrl;
        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                _avatarUrl = value;
                OnPropertyChanged(nameof(AvatarUrl));
            }
        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string PNumber
        {
            get { return _pnumber; }
            set
            {
                _pnumber = value;
                OnPropertyChanged(nameof(PNumber));
            }
        }
        public DateTime Birthdate
        {
            get { return _birthdate; }
            set
            {
                _birthdate = value;
                OnPropertyChanged(nameof(Birthdate));
            }
        }

        public ICommand UploadAvatar { get; set; }
        public ICommand Submit { get; set; }

        public UpdateProfileVM()
        {
            _firebaseRTDB = new FirebaseRTDB();
            _storageService = new FirebaseStorageService();

            string basicAvatar = "/Images/user.png";
            AvatarUrl = basicAvatar;

            //Links Command
            UploadAvatar = new RelayCommand(ExcuteUploadAvt);
            Submit = new RelayCommand(async (param) => await ExecuteSubmit(param));


        }

        //Initialize the view model
        public async Task InitAsync()
        {
            _currentUser = CurrentUserService.CurrentUser;

            string basicAvatar = "/Images/user.png";
            AvatarUrl = basicAvatar;
            if (_currentUser == null)
            {
                return;
            }
            Username = _currentUser.DisplayName ?? "";
            PNumber = _currentUser.PhoneNumber ?? "";
            Birthdate = _currentUser.Birthdate != default ? _currentUser.Birthdate : DateTime.Now;
            if (!string.IsNullOrEmpty(_currentUser.Avatar))
            {
                var url = await _storageService.GetAvatarUrlAsync(_currentUser.Avatar);
                AvatarUrl = !string.IsNullOrEmpty(url) ? url : basicAvatar;
            }
        }

        //Set the current user information
        private async Task ExecuteSubmit(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(Username))
                {

                    return;
                }

                // Update user information
                _currentUser.DisplayName = Username;
                _currentUser.PhoneNumber = PNumber;
                _currentUser.Birthdate = Birthdate;
                _currentUser.FirstLogin = true;
                //Check if the user has selected an avatar
                if (_selectedAvatarStream != null)
                {
                    //Upload avatar to Firebase Storage
                    string filename = _currentUser.Id + _selectedFileExtension;
                    var avatarUrl = await _storageService.UpLoadAvatarAsync(filename, _selectedAvatarStream);

                    _currentUser.Avatar = filename;

                    AvatarUrl = await _storageService.GetAvatarUrlAsync(filename);


                }
                //Update user information in Firebase Realtime Database
                await _firebaseRTDB.UpdateUserAsync(_currentUser);
                //Update the current user in the service
                CurrentUserService.CurrentUser = _currentUser;

                //Open MainView
                var mainView = new MainWindow();
                mainView.Show();
                //Close UpdateProfileView
                if (obj is Window updateProfileWindow)
                {
                    updateProfileWindow.Close();
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                //Dispose the stream after use
                _selectedAvatarStream?.Dispose();
            }
        }

        // Dispose the selected avatar stream when the view model is disposed
        private void ExcuteUploadAvt(object obj)
        {
            //Open file dialog to select image
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png",
            };
            bool? result = openFileDialog.ShowDialog(); // Nullable<bool>
            if (result == true)
            {
                _selectedAvatarStream?.Dispose(); // Dispose the previous stream if it exists
                //Get the selected file stream
                _selectedAvatarStream = File.OpenRead(openFileDialog.FileName);

                _selectedFileExtension = Path.GetExtension(openFileDialog.FileName);

                AvatarUrl = openFileDialog.FileName;
                OnPropertyChanged(nameof(AvatarUrl));


            }
        }

    }
}
