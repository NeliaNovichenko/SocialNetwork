using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.WEB.Models
{
    public class ChatViewModel
    {
        public List<UserProfileViewModel> Users { get; set; }
        public List<MessageViewModel> Messages { get; set; }
    }
}