using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using Readooks.DataAccessLayer.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

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
