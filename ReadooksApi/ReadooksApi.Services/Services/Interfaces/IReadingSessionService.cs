using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using Readooks.BusinessLogicLayer.Helpers;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IReadingSessionService
    {
        public Task<AddingReadingSessionResponse> AddAsync(AddingReadingSessionDto readingSessionDto);
    }
}
