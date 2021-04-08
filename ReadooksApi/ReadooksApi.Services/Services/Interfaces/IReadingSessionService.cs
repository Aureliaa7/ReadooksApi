using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IReadingSessionService
    {
        public Task<ReadingSessionDto> AddAsync(AddingReadingSessionDto readingSessionDto);
    }
}
