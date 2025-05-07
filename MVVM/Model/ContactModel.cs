using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubblyChat.MVVM.Model
{
    public class ContactModel
    {
        public string Username { get; set; }
        public string ImageSource { get; set; }

        // Khởi tạo Messages ngay khi tạo ContactModel
        public ObservableCollection<MessageModel> Messages { get; set; } = new ObservableCollection<MessageModel>();

        // Sửa LastMessage để tránh exception nếu Messages rỗng
        public string LastMessage => Messages != null && Messages.Any() ? Messages.Last().Message : "No messages yet";
    }
}

