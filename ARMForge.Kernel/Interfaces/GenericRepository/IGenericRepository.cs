using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ARMForge.Kernel.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Net.Mail;

namespace ARMForge.Kernel.Interfaces.GenericRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetByConditionAsync(
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        );
        Task<int> SaveChangesAsync();
        Task<T> AttachAsync(T entity);

        Task<List<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetQueryable(bool includeInactive = false);
    }
}
