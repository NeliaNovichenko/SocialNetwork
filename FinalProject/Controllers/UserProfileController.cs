using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Web.Mvc;
using FinalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinalProject.Controllers
{
    public class UserProfileController : Controller
    {
        /// <summary>
        /// Application DB context
        /// </summary>
        //protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        //protected UserManager<ApplicationUser> UserManager { get; set; }

        public UserProfileController()
        {
            //this.ApplicationDbContext = new ApplicationDbContext();
            //this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        // GET: UserProfile
        public ActionResult Index()
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //return View(user);
            return View();
        }

        // To convert the Byte Array to the author Image
        public FileContentResult GetProfileImage()
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            byte[] byteArray = new byte[0];
            //if (user.ProfileImage is null || user.ProfileImage.Length == 0)
            //{
                String fileName = "~/Content/Images/defaultProfileImage.jpg";
                fileName = Server.MapPath(fileName);
                if (System.IO.File.Exists(fileName))
                {
                    Image img = new Bitmap(fileName);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Close();

                        byteArray = stream.ToArray();
                    }
                }
            //}
            //else
            //    byteArray = user.ProfileImage;

            ////Convert byte arry to base64string   
            //string imreBase64Data = Convert.ToBase64String(byteArray);
            //string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            ////Passing image data in viewbag to view  
            //ViewBag.ProfileImage = imgDataURL;

            if (byteArray == null)
                return null;
            FileContentResult result = new FileContentResult(byteArray, "image/jpeg");
            return result;

        }

        [HttpPost]
        public ActionResult AddNewPost(string text)
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //using (var context = ApplicationDbContext.Create())
            //{
            //    Repository<ApplicationUser> userRepo = new Repository<ApplicationUser>(ApplicationDbContext);
            //    Repository<Post> postsRepo = new Repository<Post>(ApplicationDbContext);

            //    Post post = new Post
            //    {
            //        //ApplicationUserId = user.Id,
            //        //ApplicationUser = user,
            //        PostDate = DateTime.Now,
            //        Text = text
            //    };

            //    user.UserPosts.Add(post);

            //    postsRepo.Update(post);
            //    userRepo.Update(user);
            //}
            //return View("Index", user);
            return View("Index");
        }

        public ActionResult EditUserInfoPage()
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //return View(user);
            return View();
        }
    }
}