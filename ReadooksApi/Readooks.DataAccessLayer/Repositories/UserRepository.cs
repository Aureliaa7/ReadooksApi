using Microsoft.EntityFrameworkCore;
using Readooks.DataAccessLayer.DatabaseContext;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return Context.Set<User>()
                .Where(x => x.Email.Equals(email))
                .FirstOrDefaultAsync();
        }
    }
}
