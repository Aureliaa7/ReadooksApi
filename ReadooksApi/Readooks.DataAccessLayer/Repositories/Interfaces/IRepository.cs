using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> RemoveAsync(Guid id);
        Task<T> UpdateAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
