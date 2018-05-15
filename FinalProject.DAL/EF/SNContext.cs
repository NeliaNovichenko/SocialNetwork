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
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Post> Posts { get; set; }

        static SnContext()
        {
            Database.SetInitializer<SnContext>(new SnDbInitializer());
        }
        public SnContext(string connectionString)
                : base(connectionString)
        {
        }

    }

    public class SnDbInitializer : DropCreateDatabaseIfModelChanges<SnContext>
    {
        protected override void Seed(SnContext db)
        {
            //db.ApplicationUsers.Add(new ApplicationUser() { Name = "Nokia Lumia 630", Company = "Nokia", Price = 220 });
            //db.Phones.Add(new Phone { Name = "iPhone 6", Company = "Apple", Price = 320 });
            //db.Phones.Add(new Phone { Name = "LG G4", Company = "lG", Price = 260 });
            //db.Phones.Add(new Phone { Name = "Samsung Galaxy S 6", Company = "Samsung", Price = 300 });
            db.SaveChanges();
        }
    }
}
