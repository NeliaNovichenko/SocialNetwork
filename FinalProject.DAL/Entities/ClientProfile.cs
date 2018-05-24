using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
    public class ClientProfile
    {
        public ClientProfile()
        {
            Friends = new List<ClientProfile>();
            UserPosts = new List<Post>();
        }

        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public byte[] ProfileImage { get; set; }

        public string Gender { get; set; }

        public List<ClientProfile> Friends { get; set; }

        public List<Post> UserPosts { get; set; }

        public List<Chat> Chats { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
