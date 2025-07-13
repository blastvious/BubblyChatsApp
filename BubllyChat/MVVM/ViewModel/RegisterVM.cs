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
    public class RegisterVM : ViewModelBase
    {
        private string _email;
        private SecureString _password;
        private SecureString _confirmPassword;
        private bool _isViewVisible = true;
        private string _messageError;
        private string _colorStatusMessage = "#D75960";
        private readonly FirebaseAuthService _authService = new FirebaseAuthService();
        public string MessageError
        {
            get { return _messageError; }
            set
            {
                _messageError = value;
                OnPropertyChanged(nameof(MessageError));
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
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public SecureString ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
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



        public ICommand SignInCommand { get; }
        public ICommand RegisterCommand { get; set; }

        public RegisterVM()
        {
            SignInCommand = new RelayCommand(ExecuteSignInCommand);
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
        }
        //Constraint the user to enter a valid email and password
        private bool CanExecuteRegisterCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(Email)) return false;
            if (Password == null || ConfirmPassword == null) return false;

            string pw = FirebaseAuthService.SecureStringToString(Password);
            string cpw = FirebaseAuthService.SecureStringToString(ConfirmPassword);
            return pw == cpw && pw.Length >= 6;
        }

        private async void ExecuteRegisterCommand(object obj)
        {
            var _password = FirebaseAuthService.SecureStringToString(Password);
            var _user = await _authService.RegisterUserAsync(Email, _password);
            if (_user != null)
            {

                MessageError = "Đăng ký thành công";
                ColorStatusMessage = "Green";
                await Task.Delay(2000);
                var loginView = new LoginView();
                loginView.Show();
                if (obj is Window registerWindow)
                {
                    registerWindow.Close();
                }
            }
            else
            {
                _messageError = _authService._messageError;
            }
        }

        private void ExecuteSignInCommand(object obj)
        {
            var loginView = new LoginView();
            loginView.Show();

            if (obj is Window registerWindow)
            {
                registerWindow.Close();
            }
        }
    }
}
