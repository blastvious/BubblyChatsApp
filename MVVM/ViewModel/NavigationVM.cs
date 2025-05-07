using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BubblyChat.MVVM.ViewModel;
using BubblyChat.Core;

namespace BubblyChat.MVVM.ViewModel
{
    class NavigationVM : Core.ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ChatCommand { get; set; }
        public ICommand HomeCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public ICommand LoginCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Chat(object obj) => CurrentView = new ChattingVM();

        public NavigationVM()
        {
            ChatCommand = new RelayCommand(Chat);
            HomeCommand = new RelayCommand(Home);

            // Khởi tạo view mặc định
            CurrentView = new HomeVM();
        }
    }
}
