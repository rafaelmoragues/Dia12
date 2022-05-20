using System.Collections.Generic;
using Dia12.Repository;
namespace Dia12.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepo StudentRepo { get; }
        IGradeRepo GradeRepo { get; }
        void SaveChanges();
    }
}
