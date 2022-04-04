using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Praktika.Data.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
    }
}