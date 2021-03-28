using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.MappingConfigurations.ReadingSessions
{
    public class ReadingSessionProfile: Profile
    {
        public ReadingSessionProfile()
        {
            CreateMap<ReadingSession, ReadingSessionDto>();
        }
    }
}
