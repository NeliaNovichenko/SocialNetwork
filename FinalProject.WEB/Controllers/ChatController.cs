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

        public ActionResult StartChat(string u1, string u2)
        {
            ChatViewModel chat = new ChatViewModel();

            chat = new ChatViewModel();
            var user1Dto = userService.GetUser(u1);
            var user1 = new ChatUserViewModel()
            {
                FirstName = user1Dto.FirstName,
                LastName = user1Dto.LastName,
                Id = user1Dto.Id
            };
            chat.ChatUsers.Add(user1);

            var user2Dto = userService.GetUser(u2);
            var user2 = new ChatUserViewModel()
            {
                FirstName = user2Dto.FirstName,
                LastName = user2Dto.LastName,
                Id = user2Dto.Id
            };
            chat.ChatUsers.Add(user2);

            return View("Chat", chat);
        }

        public ActionResult OpenChat(ChatUserViewModel u1, ChatUserViewModel u2, ChatViewModel chat = null)
        {
            if (chat == null)
            {
                chat = new ChatViewModel();
                chat.ChatUsers.Add(u1);
                chat.ChatUsers.Add(u2);

            }

            return View("Chat", chat);
        }


        [HttpPost]
        public ActionResult SentMassage(ChatViewModel chatViewModel, string text)
        {
            //ChatDto chatDto = Mapper.Map<ChatViewModel, ChatDto>(chatViewModel);
            var userDTO = userService.GetUser(User.Identity.GetUserId());

            ChatMessageViewModel message = new ChatMessageViewModel()
            {
                User = new ChatUserViewModel()
                {
                    Id = userDTO.Id,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName
                },

                Text = text,
                Date = DateTime.Now
            };

            chatViewModel.Messages.Add(message);
            //chatDto.Messages.Add(messageDto);
            //userService.UpdateChat(chatDto);

            return PartialView("History", chatViewModel);
        }
    }
}