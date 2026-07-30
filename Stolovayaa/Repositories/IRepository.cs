using System.Collections.Generic;

    namespace Stolovayaa.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}