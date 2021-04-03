using Readooks.BusinessLogicLayer.Dtos.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> AddAsync(AddingBookDto bookDto);
        Task<BookDto> GetAsync(Guid readerId, Guid bookId);
        Task<IEnumerable<BookDto>> GetByReaderIdAsync(Guid readerId);
        Task DeleteAsync(Guid bookId, Guid readerId);
        Task<BookDto> UpdateAsync(Guid bookId, UpdateBookDto bookDto);
        Task<IEnumerable<BookDto>> GetByStatusAsync(Guid readerId, int status);
    }
}
