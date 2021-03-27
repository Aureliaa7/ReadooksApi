using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.Users;
using Readooks.DataAccessLayer.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readooks.BusinessLogicLayer.MappingConfigurations.Users
{
    public class UserInfoProfile: Profile
    {
        public UserInfoProfile()
        {
            CreateMap<User, UserInfoDto>();
        }
    }
}
