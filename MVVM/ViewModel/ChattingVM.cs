 using BubblyChat.Core;
using BubblyChat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubblyChat.MVVM.ViewModel
{
    public class ChattingVM : ViewModelBase
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public User OwnUser { get; set; }

        /* Commands */
        public RelayCommand SendCommand { get; set; }
        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
                //    Messages = _selectedContact?.Messages ?? new ObservableCollection<MessageModel>();
                //    OnPropertyChanged(nameof(Messages));
                //
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();

            }
        }

        public ChattingVM()
        {

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false,


                });
                Message = "";

            });
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            Messages.Add(new MessageModel
            {
                Username = "Tuan",
                UsernameColor = "#409aff",
                ImageSource = "/Images/1.jpg",
                Message = "Hello",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Nghia",
                    UsernameColor = "#409aff",
                    ImageSource = "/Images/1.jpg",
                    Message = "Hello",
                    Time = DateTime.Now,
                    IsNativeOrigin = false,
                    FirstMessage = false
                });
            }

            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "An",
                    UsernameColor = "#409aff",
                    ImageSource = "/Images/1.jpg",
                    Message = "Hello",
                    Time = DateTime.Now,
                    IsNativeOrigin = true,
                });
            }
            Messages.Add(new MessageModel
            {
                Username = "An",
                UsernameColor = "#409aff",
                ImageSource = "/Images/1.jpg",
                Message = "Last",
                Time = DateTime.Now,
                IsNativeOrigin = true,
            });

            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Tuan{i}",
                    ImageSource = "/Images/1.jpg",
                    Messages = Messages
                });
            }

        }
    }
}
