using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.Repositories.Interfaces;

namespace Readooks.DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IBookRepository BookRepository { get; }
        IReadingSessionRepository ReadingSessionRepository { get; }
    }
}
