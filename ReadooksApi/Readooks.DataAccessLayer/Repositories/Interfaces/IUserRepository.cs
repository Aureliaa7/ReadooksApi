using Readooks.DataAccessLayer.DomainEntities;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
