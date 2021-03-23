using Readooks.BusinessLogicLayer.Dtos.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> AddAsync(AddingBookDto bookDto);
        Task<BookDto> GetByIdAsync(Guid id);
        Task<IEnumerable<BookDto>> GetByReaderIdAsync(Guid readerId);
        Task DeleteAsync(Guid bookId, Guid readerId);
    }
}
