using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinalProject.DAL.EF
{
    //наследуется от класса IdentityDbContext и поэтому уже имеет свойства Users и Roles
    public class SnContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public SnContext(string connectionString)
                : base(connectionString)
        {
        }

        public SnContext() {}

    }

    public class SnDbInitializer : DropCreateDatabaseIfModelChanges<SnContext>
    {
        protected override void Seed(SnContext db)
        {
            db.SaveChanges();
        }
    }
}
