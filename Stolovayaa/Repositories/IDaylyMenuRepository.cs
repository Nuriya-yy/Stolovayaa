using System.Collections.Generic;
using Stolovayaa.Model;

namespace Stolovayaa.Repositories
{
    public interface IDaylyMenuRepository
    {
        List<DaylyMenu> GetAll();
        List<Dishes> GetDishesByMenuId(int menuId);
    }
}