using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;

namespace FinalProject.BLL.DTO
{
    public class UserDto
    {
        public UserDto()
        {
            Friends = new List<UserDto>();
            UserPosts =  new List<PostDto>();
        }
        public string Id { get; set; }
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public byte[] ProfileImage { get; set; }

        public string Gender { get; set; }

        public List<UserDto> Friends { get; set; }

        public List<PostDto> UserPosts { get; set; }
    }
}
