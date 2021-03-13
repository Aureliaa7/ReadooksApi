using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Remove(Guid id);
        Task<T> Update(T entity);
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
