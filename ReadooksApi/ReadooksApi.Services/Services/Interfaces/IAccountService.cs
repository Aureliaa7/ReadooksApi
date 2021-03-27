﻿using Readooks.BusinessLogicLayer.Dtos.Users;
using System;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> LoginAsync(UserLoginDto userLoginDto);
        Task<UserDto> RegisterAsync(UserRegistrationDto userRegisterDto);
        Task<UserInfoDto> GetInfo(Guid userId);
    }
}
