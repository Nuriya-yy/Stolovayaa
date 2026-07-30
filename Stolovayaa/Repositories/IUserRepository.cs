using Stolovayaa.Model;
using Stolovayaa.Repositories;

namespace Stolovayaa.Repositories
{
    public interface IUserRepository : IRepository<Users>
    {
        Users GetUserByLogin(string login);
        Users GetUserByLoginAndPassword(string login, string password);
        bool IsLoginExists(string login);
        Users GetUserWithRole(int userId);
    }
}