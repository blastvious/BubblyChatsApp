using BubblyChat.Core;
using BubblyChat.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BubblyChat.MVVM.ViewModel
{
    public class LoginVM : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
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
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayCommand(p=>ExecuteRecoverPasswordCommand("",""));
            SignUpCommand = new RelayCommand(ExecuteSignUpCommand);

        }

        private void ExecuteSignUpCommand(object obj)
        {
            IsViewVisible = false;
            var registerView = new RegisterView();
            registerView.Show();
            if(obj is Window loginWindow)
            {
                loginWindow.Close();
            }
        }

        private void ExecuteRecoverPasswordCommand(string usernmae, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object arg)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||  Password==null || Password.Length<3)
            {
                validData = false;
            }
            else
            {
                validData = true;   
            }
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
