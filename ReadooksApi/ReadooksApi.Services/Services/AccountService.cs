using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.Services.PasswordEncryption;
using Readooks.BusinessLogicLayer.Dtos;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

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
            var users = await unitOfWork.UserRepository.GetAll();
            var userDtos = new List<UserDto>();

            foreach(var user in users)
            {
                userDtos.Add(mapper.Map<UserDto>(user));
            }

            return userDtos;
        }

        public Task Login(UserLoginDto userLoginVm)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> Register(UserRegistrationDto userRegisterDto)
        {
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
                user = await unitOfWork.UserRepository.Add(newUser);
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
