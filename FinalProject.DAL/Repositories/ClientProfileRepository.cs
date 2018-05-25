using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Interfaces;

namespace FinalProject.DAL.Repositories
{
    class ClientProfileRepository : IRepository<ClientProfile>
    {
        DbContext _context;
        DbSet<ClientProfile> _dbSet;

        public ClientProfileRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<ClientProfile>();
        }
        public void Create(ClientProfile item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate)
        {
            return _dbSet.AsNoTracking()
                .Include(c => c.Friends)
                .Include(c => c.UserPosts)
                .Include(c => c.Messages)
                .Where(predicate).ToList();
        }

        public ClientProfile Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            var result = _dbSet.AsNoTracking()
                .Include(c => c.Friends)
                .Include(c => c.UserPosts)
                .Include(c => c.Messages)
                .ToList();
            return result;
        }

        public void Update(ClientProfile item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
