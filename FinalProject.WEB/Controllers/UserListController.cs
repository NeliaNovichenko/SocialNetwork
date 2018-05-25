using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FinalProject.BLL.DTO;
using FinalProject.BLL.Interfaces;
using FinalProject.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FinalProject.WEB.Controllers
{
    public class UserListController : Controller
    {
        private IUserService userService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        public ActionResult Index()
        {
            var users = userService.GetAllUsers();
            var profiles = Mapper.Map<IEnumerable<ClientProfileDto>, List<UserProfileViewModel>>(users);
            return View(profiles);
        }

        public ActionResult ShowUsers(List<UserProfileViewModel> users)
        {
            return View("Index", users);
        }
    }
}