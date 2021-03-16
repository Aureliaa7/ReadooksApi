using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos;
using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.MappingConfigurations
{
    public class UserRegistrationProfile : Profile
    {
        public UserRegistrationProfile()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
