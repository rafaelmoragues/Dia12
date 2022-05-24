using Dia12.Data;
using Dia12.Models;

namespace Dia12.Repository
{
    public class GradeRepo : GenericRepository<Grade>, IGradeRepo
    {
        public GradeRepo(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }


    }
}
