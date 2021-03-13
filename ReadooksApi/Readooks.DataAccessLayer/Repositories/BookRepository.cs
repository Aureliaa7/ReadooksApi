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
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Book> GetWithRelatedEntities(Guid id)
        {
            return await Context.Set<Book>()
                .Where(x => x.Id == id)
                .Include(x => x.Reader)
                .FirstAsync();
        }

        public async Task<IEnumerable<Book>> GetWithRelatedEntitiesByUser(Guid userId)
        {
            return await Context.Set<Book>()
               .Where(x => x.Reader.Id == userId)
               .Include(x => x.Reader)
               .ToListAsync();
        }
    }
}
