using Dia12.Data;
using Microsoft.EntityFrameworkCore;

namespace Dia12.Repository
{
    public class GenericRepository <T>: IGenericRepository<T> where T : class

    {
        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task Delete(int? id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new Exception("No se encontro objeto");
            _dbContext.Set<T>().Remove(entity);

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T>  GetById(int? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Insert(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task Update(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}
