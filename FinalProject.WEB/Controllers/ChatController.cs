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
    public class ChatController : Controller
    {
        private IUserService userService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        public ActionResult OpenChat(string u1, string u2)
        {
            var user1Dto = userService.GetUser(u1);
            var user2Dto = userService.GetUser(u2);

            var messagesDto = userService.GetMessages(u1, u2).ToList();
            var messagesViewModel = Mapper.Map<IEnumerable<MessageDto>, IEnumerable<MessageViewModel>>(messagesDto).ToList();

            ChatViewModel chat = new ChatViewModel()
            {
                Users = new List<UserProfileViewModel>(),
                Messages = messagesViewModel
            };
            
            return View("Chat", messagesViewModel.ToList());
        }


        [HttpPost]
        public ActionResult SentMassage(string u1, string u2, string text)
        {
            var user1Dto = userService.GetUser(u1);
            var user2Dto = userService.GetUser(u2);

            var users = new List<ClientProfileDto>();
            users.Add(user1Dto);
            users.Add(user2Dto);

            MessageDto message = new MessageDto()
            {
                Text = text,
                Date = DateTime.Now,
                UserDtos = users
            };

            userService.AddMessage(message);
            var messagesDto = userService.GetMessages(u1, u2).ToList();
            var messagesViewModel = Mapper.Map<IEnumerable<MessageDto>, IEnumerable<MessageViewModel>>(messagesDto);

            return PartialView("History", messagesViewModel.ToList());
        }
    }
}