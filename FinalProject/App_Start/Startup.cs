using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.BLL.Interfaces;
using FinalProject.BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

//[assembly: OwinStartup(typeof(UserStore.App_Start.Startup))]


namespace FinalProject.App_Start
{
   
    public class Startup
    {
        //С помощью фабрики сервисов здесь создается сервис для работы с сервисами:
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            //Потом сервис региструется контекстом OWIN:
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            //в файле web.config имеется строка подключения DefaultConnection, которая передается в метод 
            return serviceCreator.CreateUserService("DefaultConnection");
        }
    }
}