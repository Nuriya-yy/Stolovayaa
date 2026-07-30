using System.Data.Entity;
using System.Linq;          
using Stolovayaa.Model;

namespace Stolovayaa.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(Dining_roomEntities1 context) : base(context)
        {

        }

        public Users GetUserByLogin(string login)
        {
            return _dbSet.FirstOrDefault(u => u.Login == login);
        }

        public Users GetUserByLoginAndPassword(string login, string password)
        {
            return _dbSet.FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public bool IsLoginExists(string login)
        {
            return _dbSet.Any(u => u.Login == login);
        }

        public Users GetUserWithRole(int userId)
        {
            return _dbSet.Include("Roles").FirstOrDefault(u => u.ID == userId);
        }
    }
}