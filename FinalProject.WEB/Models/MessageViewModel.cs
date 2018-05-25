using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace FinalProject.WEB.Models
{
    public class MessageViewModel
    {
        public List<UserProfileViewModel> ChatUsers { get; set; }

        public DateTime Date = DateTime.Now;
        public string Text { get; set; }
    }

}