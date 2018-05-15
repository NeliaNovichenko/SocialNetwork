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
    class PostRepository : IRepository<Post>
    {
        private SnContext db;

        public PostRepository(SnContext context)
        {
            this.db = context;
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts;
        }

        public Post Get(int id)
        {
            return db.Posts.Find(id);
        }

        public void Create(Post appUser)
        {
            db.Posts.Add(appUser);
        }

        public void Update(Post book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<Post> Find(Func<Post, Boolean> predicate)
        {
            return db.Posts.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Post book = db.Posts.Find(id);
            if (book != null)
                db.Posts.Remove(book);
        }
    }
}
