using Readooks.DataAccessLayer.DatabaseContext;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.Repositories.Interfaces;

namespace Readooks.DataAccessLayer.Repositories
{
    public class ReadingSessionRepository : Repository<ReadingSession>, IReadingSessionRepository
    {
        public ReadingSessionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
