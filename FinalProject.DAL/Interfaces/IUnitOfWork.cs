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
        IRepository<Message> Messages { get; }
        void Save();
        Task SaveAsync();
    }
}
