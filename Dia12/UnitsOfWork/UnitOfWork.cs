using System.Collections.Generic;
using Dia12.Repository;
using Dia12.Data;
using Dia12.Controllers;


namespace Dia12.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            StudentRepo = new StudentsRepo(_context);
            GradeRepo = new GradeRepo(_context);
        }
        public IStudentRepo StudentRepo {get; private set;}
        public IGradeRepo GradeRepo { get; private set; }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
