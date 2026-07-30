using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Stolovayaa.Model;

namespace Stolovayaa.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly Dining_roomEntities1 _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(Dining_roomEntities1 context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}