using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.Services.PasswordEncryption;
using Readooks.BusinessLogicLayer.Dtos.Users;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.UnitOfWork;
using System.Threading.Tasks;
using AutoMapper;
using System;
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

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == id);
            if(userExists)
            {
                var user = await unitOfWork.UserRepository.GetAsync(id);
                return mapper.Map<UserDto>(user);
            }
            throw new NotFoundException("The user was not found");
        }

        public async Task<UserDto> LoginAsync(UserLoginDto userLogin)
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

        public async Task<UserDto> RegisterAsync(UserRegistrationDto userRegisterDto)
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
                user = await unitOfWork.UserRepository.AddAsync(newUser);
            }
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateNoCoinsAsync(Guid userId, int noCoins)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == userId);
            if (userExists)
            {
                var user = await unitOfWork.UserRepository.GetAsync(userId);
                user.NumberOfCoins = noCoins;
                var updatedUser = await unitOfWork.UserRepository.UpdateAsync(user);
                return mapper.Map<UserDto>(updatedUser);
            }
            throw new NotFoundException("The user was not found");
        }

        private async Task<bool> AccountExists(string email)
        {
            var accountExists = await unitOfWork.UserRepository.Exists(x => x.Email.Equals(email));
            return accountExists;
        }
    }
}
