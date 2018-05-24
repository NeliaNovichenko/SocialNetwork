using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FinalProject.BLL.DTO;
using FinalProject.BLL.Interfaces;
using FinalProject.BLL.Services;
using FinalProject.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FinalProject.WEB.Controllers
{
    
    public class UserProfileController : Controller
    {
        private IUserService userService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
       
        public ActionResult Index()
        {
            var userDTO = userService.GetUser(User.Identity.GetUserId());
            var profile = Mapper.Map<UserDto, UserProfileViewModel>(userDTO);

            if (profile == null)
                return RedirectToAction("Login", "Account");

            return View(profile);
        }


        public ActionResult ShowProfile([Bind(Exclude = "ProfileImage")]UserProfileViewModel profile)
        {
            if (profile == null)
                return RedirectToAction("Index", "UserList");

            return View("Index", profile);
        }

        // To convert the Byte Array to the author Image
        public FileContentResult GetProfileImage(byte[] byteArray)
        {
            if (byteArray is null || byteArray.Length == 0)
            {
                String fileName = "~/Content/Images/defaultProfileImage.jpg";
                fileName = Server.MapPath(fileName);
                if (System.IO.File.Exists(fileName))
                {
                    Image img = new Bitmap(fileName);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        byteArray = stream.ToArray();
                        stream.Close();
                        
                    }
                }
            }
            FileContentResult result = new FileContentResult(byteArray, "image/jpeg");
            return result;
        }

        [HttpPost]
        public ActionResult AddNewPost(string text)
        {
            var userId = User.Identity.GetUserId();
            PostDto post = new PostDto
            {
                ApplicationUserId = userId,
                PostDate = DateTime.Now,
                Text = text
            };
            userService.AddPost(post);
            return RedirectToAction("Index");

        }

        [Authorize]
        public ActionResult EditUserProfile()
        {
            var userDTO = userService.GetUser(User.Identity.GetUserId());
            var profile = Mapper.Map<UserDto, UserProfileViewModel>(userDTO);

            return View(profile);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditUserProfile(UserProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                var userDto = Mapper.Map<UserProfileViewModel, UserDto>(profile);
                userService.UpdateUser(userDto);
            }

            return View("Index", profile);
        }


        public ActionResult Subscribe(UserProfileViewModel followed)
        {
            var userDTO = userService.GetUser(User.Identity.GetUserId());
            var followedDto = Mapper.Map<UserProfileViewModel, UserDto>(followed);
            userDTO.Friends.Add(followedDto);
            userService.UpdateUser(userDTO);

            return RedirectToAction("ShowProfile", followed);
        }

    }
}