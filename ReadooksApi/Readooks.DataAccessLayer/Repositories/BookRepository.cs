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

        public async Task<IEnumerable<Book>> GetByReaderIdAsync(Guid readerId)
        {
            return await Context.Set<Book>()
               .Where(x => x.Reader.Id == readerId)
               .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByStatusAsync(Guid readerId, BookStatus status)
        {
            return await Context.Set<Book>()
                          .Where(x => x.Reader.Id == readerId && x.Status == status)
                          .ToListAsync();
        }
    }
}
