using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubllyChat.MVVM.Models
{
    public class Users
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public bool FirstLogin { get; set; } = false;

        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Birthdate { get; set; }
        public List<string> Friends { get; set; } = new List<string>();
        public List<string> Rooms { get; set; } = new List<string>();
    }
}
