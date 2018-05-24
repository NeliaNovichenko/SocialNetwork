using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.DTO
{
    public class PostDto
    {
        public int PostId { get; set; }

        public string Text { get; set; }
        
        public DateTime PostDate { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
