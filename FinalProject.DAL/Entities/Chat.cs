using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<string> ChatUserIds { get; set; }
        public List<Message> Messages { get; set; }

        public Chat()
        {
            ChatUserIds = new List<string>();
            Messages = new List<Message>();
        }

    }
}
