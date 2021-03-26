using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.MappingConfigurations.ReadingSessions
{
    public class AddingReadingSessionProfile: Profile
    {
        public AddingReadingSessionProfile()
        {
            CreateMap<AddingReadingSessionDto, ReadingSession>();
        }
    }
}
