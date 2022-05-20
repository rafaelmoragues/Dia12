using Dia12.Data;
using Dia12.Models;

namespace Dia12.Repository
{
    public class StudentsRepo : GenericRepository<Student>, IStudentRepo
    {
        public StudentsRepo(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

    }
}
