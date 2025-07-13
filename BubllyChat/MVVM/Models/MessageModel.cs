using BubllyChat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubllyChat.MVVM.Models
{
    public class MessageModel : ViewModelBase
    {
        public string DisplayName { get; set; }

        public string UsernameColor { get; set; } = "#409aff";

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public bool IsNativeOrigin { get; set; } = false;

        public bool? FirstMessage { get; set; }

        public string MessageType { get; set; } = "Text";

        private string _imageSender { get; set; } = string.Empty;
        public string ImageSender
        {
            get => _imageSender;
            set
            {
                _imageSender = value;
                OnPropertyChanged();
            }
        }

    }
}
