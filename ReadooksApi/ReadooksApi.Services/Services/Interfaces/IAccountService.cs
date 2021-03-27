﻿using Readooks.BusinessLogicLayer.Dtos.Users;
using System;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> LoginAsync(UserLoginDto userLoginDto);
        Task<UserDto> RegisterAsync(UserRegistrationDto userRegisterDto);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> UpdateNoCoinsAsync(Guid userId, int noCoins);
    }
}
