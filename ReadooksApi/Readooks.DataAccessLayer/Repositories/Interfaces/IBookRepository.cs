using Readooks.DataAccessLayer.DomainEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetWithRelatedEntities(Guid id);
        Task<IEnumerable<Book>> GetWithRelatedEntitiesByUser(Guid userId);
    }
}
