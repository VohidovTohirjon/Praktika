using Microsoft.EntityFrameworkCore;
using Praktika.Data.DbContexts;
using Praktika.Data.IRepository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#pragma warning disable

namespace Praktika.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal PraktikaContext prcontext;
        internal DbSet<T> dbSet;

        public GenericRepository(PraktikaContext prcontext)
        {
            this.prcontext = prcontext;
            dbSet = prcontext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression is null ? dbSet : dbSet.Where(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entry = dbSet.Update(entity);

            return entry.Entity;
        }
    }
}
