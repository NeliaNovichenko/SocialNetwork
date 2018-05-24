using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.DTO
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }
    }
}
