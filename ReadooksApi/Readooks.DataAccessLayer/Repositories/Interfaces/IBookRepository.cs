using Readooks.DataAccessLayer.DomainEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetByReaderIdAsync(Guid readerId);
        Task<IEnumerable<Book>> GetByStatusAsync(Guid readerId, BookStatus status);

    }
}
