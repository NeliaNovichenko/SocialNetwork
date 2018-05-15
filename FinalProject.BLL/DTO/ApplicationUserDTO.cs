using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;

namespace FinalProject.BLL.DTO
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public byte[] ProfileImage { get; set; }

        public string Gender { get; set; }

        public List<ApplicationUser> Friends { get; set; }

        public List<Post> UserPosts { get; set; }
    }
}
