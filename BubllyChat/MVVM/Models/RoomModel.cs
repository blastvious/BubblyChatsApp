using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubllyChat.MVVM.Models
{
    public class RoomModel : ContactModel
    {
        public string RoomId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<string> Members { get; set; } = new List<string>();

        public bool IsGroupChat => true;

        public string MemberCount => $"{Members?.Count ?? 0} members";
    }
}
