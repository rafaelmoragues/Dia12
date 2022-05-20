using NPOI.SS.Formula.Functions;

namespace Dia12.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task <T> GetById(int? id);
        Task <T> Insert(T entity);
        Task Update(T entity);
        Task Delete(int? id);
    }
}
