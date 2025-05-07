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
    public class RegisterVM : ViewModelBase
    {
        private string _email;
        private SecureString _password;
        private SecureString _confirmPassword;
        private object _currentView;
        private bool _isViewVisible = true;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
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



        public ICommand SignInCommand { get;  }
        public ICommand OKCommand { get; set; }

        public RegisterVM()
        {
            SignInCommand = new RelayCommand(ExecuteSignInCommand);
        }

     

        private void ExecuteSignInCommand(object obj)
        {
            var loginView = new LoginView();
            loginView.Show();

            if(obj is Window registerWindow)
            {
                registerWindow.Close();
            }
        }
    }
}