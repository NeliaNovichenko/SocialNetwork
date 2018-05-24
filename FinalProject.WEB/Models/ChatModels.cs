using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace FinalProject.WEB.Models
{
    public class ChatViewModel
    {
        public List<ChatMessageViewModel> Messages { get; set; }
        public List<ChatUserViewModel> ChatUsers { get; set; }

        public ChatViewModel()
        {
            ChatUsers = new List<ChatUserViewModel>();
            Messages = new List<ChatMessageViewModel>();
        }
    }

    public class ChatMessageViewModel
    {
        public ChatUserViewModel User { get; set; }
        public DateTime Date = DateTime.Now;
        public string Text { get; set; }
    }

    public class ChatUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}