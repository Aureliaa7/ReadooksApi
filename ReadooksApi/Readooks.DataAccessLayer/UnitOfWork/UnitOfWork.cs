using Readooks.DataAccessLayer.DatabaseContext;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.Repositories;
using Readooks.DataAccessLayer.Repositories.Interfaces;

namespace Readooks.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            UserRepository = new UserRepository(applicationDbContext);
            BookRepository = new BookRepository(applicationDbContext);
            ReadingSessionRepository = new ReadingSessionRepository(applicationDbContext);
        }

        public IUserRepository UserRepository { get; private set; }

        public IBookRepository BookRepository { get; private set; }

        public IReadingSessionRepository ReadingSessionRepository { get; private set; }
    }
}
