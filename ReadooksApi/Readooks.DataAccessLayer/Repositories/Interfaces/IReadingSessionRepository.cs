using Readooks.DataAccessLayer.DomainEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories.Interfaces
{
    public interface IReadingSessionRepository : IRepository<ReadingSession>
    {
        public Task<IEnumerable<ReadingSession>> GetByBookIdAsync(Guid bookId);
    }
}
