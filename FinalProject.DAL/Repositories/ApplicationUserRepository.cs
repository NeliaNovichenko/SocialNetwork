using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.EF;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Interfaces;

namespace FinalProject.DAL.Repositories
{
    public class ApplicationUserRepository : IRepository<ApplicationUser>
    {
        private SnContext db;

        public ApplicationUserRepository(SnContext context)
        {
            this.db = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users;
        }

        public ApplicationUser Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(ApplicationUser appUser)
        {
            db.Users.Add(appUser);
        }

        public void Update(ApplicationUser book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<ApplicationUser> Find(Func<ApplicationUser, Boolean> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            ApplicationUser book = db.Users.Find(id);
            if (book != null)
                db.Users.Remove(book);
        }
       
    }
}
