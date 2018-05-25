using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FinalProject.BLL.DTO;
using FinalProject.WEB.Models;

namespace FinalProject.WEB
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ClientProfileDto, UserProfileViewModel>();
                cfg.CreateMap<UserProfileViewModel, ClientProfileDto>();
                cfg.CreateMap<MessageDto, MessageViewModel>();

            });
        }
    }
}