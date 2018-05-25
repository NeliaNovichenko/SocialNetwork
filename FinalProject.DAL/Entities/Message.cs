using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public List<ClientProfile> Users { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }
    }
}
