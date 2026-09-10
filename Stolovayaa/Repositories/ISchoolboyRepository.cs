using System.Collections.Generic;
using Stolovayaa.Model;

namespace Stolovayaa.Repositories
{
    public interface ISchoolboyRepository
    {
        List<Schoolboy> GetAll();
    }
}
