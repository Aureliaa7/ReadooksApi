using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.Services.PasswordEncryption;
using Readooks.BusinessLogicLayer.Dtos;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Readooks.BusinessLogicLayer.Exceptions;

namespace Readooks.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPasswordEncryptionService passwordEncryptionService;
        private readonly IMapper mapper;

        public AccountService(IUnitOfWork unitOfWork, IPasswordEncryptionService passwordEncryptionService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.passwordEncryptionService = passwordEncryptionService;
            this.mapper = mapper;
        }

        // TODO remove this method later. For now it's used only for checking the inserted users
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await unitOfWork.UserRepository.GetAllAsync();
            var userDtos = new List<UserDto>();

            foreach(var user in users)
            {
                userDtos.Add(mapper.Map<UserDto>(user));
            }

            return userDtos;
        }

        public async Task<UserDto> Login(UserLoginDto userLogin)
        {
            UserDto userDto = null;
            bool accountExists = await AccountExists(userLogin.Email);
            if (accountExists)
            {
                var user = await unitOfWork.UserRepository.GetByEmailAsync(userLogin.Email);
                bool passwordIsCorrect = passwordEncryptionService.PasswordIsCorrect(userLogin.Password, user.Salt, user.Password);
                if(passwordIsCorrect)
                {
                    userDto = mapper.Map<UserDto>(user);
                } 
            }
            return userDto;
        }

        public async Task<UserDto> Register(UserRegistrationDto userRegisterDto)
        {
            // TODO: remove the password from the response :)
            User user = null;
            bool accountExists = await AccountExists(userRegisterDto.Email);

            if (!accountExists)
            {
                var newUser = new User
                {
                    AvailableSpotsOnBookshelf = Constants.InitialNoSpotsOnBookshelf,
                    Email = userRegisterDto.Email,
                    FirstName = userRegisterDto.FirstName,
                    LastName = userRegisterDto.LastName,
                    NumberOfCoins = 0,
                    Salt = Salt.Create()
                };
                newUser.Password = passwordEncryptionService.Encrypt(userRegisterDto.Password, newUser.Salt);
                user = await unitOfWork.UserRepository.AddAsync(newUser);
            }
            return mapper.Map<UserDto>(user);
        }

        private async Task<bool> AccountExists(string email)
        {
            var accountExists = await unitOfWork.UserRepository.Exists(x => x.Email.Equals(email));
            return accountExists;
        }
    }
}
