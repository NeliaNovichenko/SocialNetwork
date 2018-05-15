using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.EF;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Identity;
using FinalProject.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinalProject.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SnContext db;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private GenericRepository<ClientProfile> clientProfileRepository;
        private GenericRepository<Post> postRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new SnContext(connectionString);
        }

        public ApplicationUserManager UserManager => userManager ?? (userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db)));

        public ApplicationRoleManager RoleManager => roleManager ?? (roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db)));

        public IRepository<ClientProfile> ClientProfiles => clientProfileRepository ?? (clientProfileRepository =
                                                                    new GenericRepository<ClientProfile>(db));
    
        public IRepository<Post> Posts => postRepository ?? (postRepository = new GenericRepository<Post>(db));

        public void Save()
        {
            db.SaveChanges();
        }
        
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
