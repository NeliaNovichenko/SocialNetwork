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
    public class SnContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Message> Messages { get; set; }

        public SnContext(string connectionString)
                : base(connectionString)
        {
        }

        public SnContext() {}

    }

}
