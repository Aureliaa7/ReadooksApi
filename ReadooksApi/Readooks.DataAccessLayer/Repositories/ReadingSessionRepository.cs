using Microsoft.EntityFrameworkCore;
using Readooks.DataAccessLayer.DatabaseContext;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories
{
    public class ReadingSessionRepository : Repository<ReadingSession>, IReadingSessionRepository
    {
        public ReadingSessionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<ReadingSession>> GetByBookIdAsync(Guid bookId)
        {
            return await Context.Set<ReadingSession>()
               .Where(x => x.Book.Id == bookId)
               .ToListAsync();
        }
    }
}
