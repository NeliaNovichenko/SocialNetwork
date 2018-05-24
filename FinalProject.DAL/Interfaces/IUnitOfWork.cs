using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Identity;

namespace FinalProject.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IRepository<ClientProfile> ClientProfiles { get; }
        IRepository<Chat> Chats { get; }
        void Save();
        Task SaveAsync();

        //https://metanit.com/sharp/mvc5/23.10.php
        //ApplicationUserManager UserManager { get; }
        //IClientManager ClientManager { get; }
        //ApplicationRoleManager RoleManager { get; }
        //Task SaveAsync();


    }
}
