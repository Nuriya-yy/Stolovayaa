using System.Collections.Generic;
using System.Linq;
using Stolovayaa.Model;

namespace Stolovayaa.Repositories
{
    public class DaylyMenuRepository : BaseRepository<DaylyMenu>, IDaylyMenuRepository
    {
        public DaylyMenuRepository(Dining_roomEntities1 context) : base(context) { }

        public List<DaylyMenu> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<Dishes> GetDishesByMenuId(int menuId)
        {
            var menu = _dbSet.Include("Dishes").FirstOrDefault(m => m.ID == menuId);
            if (menu == null) return new List<Dishes>();
            return new List<Dishes> { menu.Dishes };
        }
    }
}