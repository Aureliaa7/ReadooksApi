using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IReadingSessionService
    {
        public Task<ReadingSessionDto> AddAsync(AddingReadingSessionDto readingSessionDto);
        public Task<IEnumerable<ReadingSessionDto>> GetByBookIdAsync(Guid bookId);
    }
}
