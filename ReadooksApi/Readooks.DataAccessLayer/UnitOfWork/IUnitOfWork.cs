using Readooks.DataAccessLayer.Repositories.Interfaces;

namespace Readooks.DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IBookRepository BookRepository { get; }
        IReadingSessionRepository ReadingSessionRepository { get; }
    }
}
