using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.BLL.DTO;

namespace FinalProject.WEB.Models
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            if (Friends is null)
                Friends = new List<UserProfileViewModel>();
            if (UserPosts is null)
                UserPosts = new List<PostDto>();
        }

        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [DisplayName("Phone number")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string PhoneNumber { get; set; }

        [DisplayName("Date of birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        public byte[] ProfileImage { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        public List<UserProfileViewModel> Friends { get; set; }

        public List<PostDto> UserPosts { get; set; }
    }
}