using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinalProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Friends = new List<ApplicationUser>();
            UserPosts = new List<Post>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateOfBirth { get; set; }

        public byte[] ProfileImage { get; set; }

        public string Gender { get; set; }

        public List<ApplicationUser> Friends { get; set; }

        public List<Post> UserPosts { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public string Text { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PostDate { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Post> Posts { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }

    //public class UserDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    //{
    //protected override void Seed(ApplicationDbContext db)
    //{
    //    db.Posts.Add(new Post()
    //    {
    //        Text = "Post 1",
    //        PostDate = DateTime.Now
    //    });
    //    db.Posts.Add(new Post()
    //    {
    //        Text = "Post 2",
    //        PostDate = DateTime.Now
    //    });
    //    db.Posts.Add(new Post()
    //    {
    //        Text = "Post 3",
    //        PostDate = DateTime.Now
    //    });
    //    db.Posts.Add(new Post()
    //    {
    //        Text = "Post 4",
    //        PostDate = DateTime.Now
    //    });

    //    db.Users.Add(new ApplicationUser
    //    {
    //        FirstName = "Vanya",
    //        LastName = "Gafyak",
    //        DateOfBirth = new DateTime(2000, 1, 17),
    //        ProfileImage = null,
    //        Gender = "Male",
    //        Friends = new List<ApplicationUser>(db.Users.Where(u=> u.FirstName == "Novichenko")),
    //        UserPosts = new List<Post>(db.Posts.Where(p=>p.PostId == 1 || p.PostId == 2))
    //    });
    //    ApplicationUser user = db.Users.Where(u => u.Email == "novichenko.nelya@gmail.com").FirstOrDefault();
    //    //db.ApplicationUsers.Add(new ApplicationUser
    //    //{
    //    //    Email = "novichenko.nelya@gmail.com",
    //    //    FirstName = "Nelya",
    //    //    LastName = "Novichenko",
    //    //    DateOfBirth = new DateTime(2000, 1, 16),
    //    //    ProfileImage = null,
    //    //    Gender = "Female",
    //    //    Friends = new List<ApplicationUser>(db.ApplicationUsers.Where(u => u.FirstName == "Gafyak")),
    //    //    UserPosts = new List<Post>(db.Posts.Where(p => p.PostId == 3 || p.PostId == 4))
    //    //});

    //    user.FirstName = "Nelya";
    //    user.LastName = "Novichenko";
    //    user.DateOfBirth = new DateTime(2000, 1, 16);
    //    user.ProfileImage = null;
    //    user.Gender = "Female";
    //    user.Friends = new List<ApplicationUser>(db.Users.Where(u => u.FirstName == "Gafyak"));
    //    user.UserPosts = new List<Post>(db.Posts.Where(p => p.PostId == 3 || p.PostId == 4));

    //    db.SaveChanges();
    //}
    //}
}