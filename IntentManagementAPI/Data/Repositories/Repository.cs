using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IntentManagementAPI.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IntentManagementContext _context;

        public Repository(IntentManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (ArgumentException)
            {
                // Entity not found or invalid ID format - silently ignore
                return;
            }
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> GetNextIdAsync()
        {
            var entityType = typeof(T);
            var primaryKey = _context.Model.FindEntityType(entityType)?.FindPrimaryKey();
            if (primaryKey == null)
                throw new InvalidOperationException("Entity type does not have a primary key");

            var keyType = primaryKey.Properties[0].ClrType;
            if (keyType == typeof(int))
            {
                var maxId = _context.Set<T>().AsEnumerable().Max(e => (int?)primaryKey.Properties[0].PropertyInfo.GetValue(e)) ?? 0;
                return maxId + 1;
            }
            throw new NotSupportedException($"Unsupported primary key type: {keyType}");
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var entityType = typeof(T);
            var primaryKey = _context.Model.FindEntityType(entityType)?.FindPrimaryKey();
            if (primaryKey == null)
                throw new InvalidOperationException("Entity type does not have a primary key");

            var keyType = primaryKey.Properties[0].ClrType;
            if (keyType == typeof(string))
            {
                return await _context.Set<T>().FindAsync(id);
            }
            else if (keyType == typeof(int))
            {
                if (int.TryParse(id, out int intId))
                {
                    return await _context.Set<T>().FindAsync(intId);
                }
                else
                {
                    throw new ArgumentException($"Cannot parse '{id}' as integer for primary key");
                }
            }
            throw new ArgumentException($"Unsupported primary key type: {keyType}");
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 