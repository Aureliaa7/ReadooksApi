using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.Users;
using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.MappingConfigurations.Users
{
    public class UserRegistrationProfile : Profile
    {
        public UserRegistrationProfile()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
