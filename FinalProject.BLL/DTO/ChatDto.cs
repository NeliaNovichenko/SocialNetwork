using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.DTO
{
    public class ChatDto
    {
        public string Id { get; set; }
        public List<string> ChatUserIds { get; set; }
        public List<MessageDto> Messages { get; set; }

        public ChatDto()
        {
            ChatUserIds = new List<string>();
            Messages = new List<MessageDto>();
        }
    }

}
