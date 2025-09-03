using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ARMForge.Kernel.Entities;

namespace ARMForge.Kernel.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<int> SaveChangesAsync();
    }
}
