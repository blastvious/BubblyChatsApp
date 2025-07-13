
using BubllyChat.Core;
using BubllyChat.MVVM.View;
using BubllyChat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BubllyChat.MVVM.ViewModel
{
    public class LoginVM : ViewModelBase
    {
        private string _email;
        private SecureString _password;
        private string _statusMessage;
        private bool _isViewVisible = true;
        private string _colorStatusMessage = "#D75960";
        private readonly FirebaseAuthService _authService = new FirebaseAuthService();


        public string ColorStatusMessage
        {
            get { return _colorStatusMessage; }
            set
            {
                _colorStatusMessage = value;
                OnPropertyChanged(nameof(ColorStatusMessage));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }


        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
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
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }



        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        public ICommand RecoverPasswordCommand { get; }

        public ICommand ShowPasswordCommand { get; }

        public ICommand RememberPasswordCommand { get; }

        public LoginVM()
        {
            LoginCommand = new RelayCommand(ExecuteLoginCommandAsync, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayCommand(ExecuteRecoverPasswordCommand);
            SignUpCommand = new RelayCommand(ExecuteSignUpCommand);

        }


        //Constrant for login Length must be over 6 characters
        private bool CanExecuteLoginCommand(object arg)
        {
            return !string.IsNullOrWhiteSpace(Email)
                && Password != null
                && Password.Length >= 6;
        }

        private async void ExecuteLoginCommandAsync(object obj)
        {
            var password = FirebaseAuthService.SecureStringToString(Password);
            var user = await _authService.LoginUserAsync(Email, password);

            if (user != null)
            {
                ColorStatusMessage = "Green";
                StatusMessage = "Đăng nhập thành công!";
                CurrentUserService.CurrentUser = user;

                await Task.Delay(2000);
                var UpateProfileView = new UpdateProfileView();
                UpateProfileView.Show();
                if (obj is Window loginWindow)
                    loginWindow.Close();
            }
            else
            {
                ColorStatusMessage = "#D75960";
                StatusMessage = "Sai mật khẩu";
            }
        }

        private void ExecuteSignUpCommand(object obj)
        {
            IsViewVisible = false;
            var registerView = new RegisterView();
            registerView.Show();
            if (obj is Window loginWindow)
            {
                loginWindow.Close();
            }
        }

        private async void ExecuteRecoverPasswordCommand(object obj)
        {
            var success = await _authService.SendPasswordResetEmailAsync(Email);

            if (success)
            {
                ColorStatusMessage = "Green";
                StatusMessage = "Đã gửi email đặt lại mật khẩu đến " + Email;
                return;
            }
            else
            {
                ColorStatusMessage = "#D75960";
                StatusMessage = "Email không hợp lệ";
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }


    }
}
