using System.Collections.Generic;
using System.Linq;
using Stolovayaa.Model;

namespace Stolovayaa.Repositories
{
    public class SchoolboyRepository : BaseRepository<Schoolboy>, ISchoolboyRepository
    {
        public SchoolboyRepository(Dining_roomEntities1 context) : base(context) { }

        public List<Schoolboy> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}