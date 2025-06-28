using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntentManagementAPI.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<int> GetNextIdAsync();
        void Remove(T entity);
        Task SaveChangesAsync();
    }
} 