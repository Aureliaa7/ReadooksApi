﻿using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos;
using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.MappingConfigurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
