using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.ViewModels;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await unitOfWork.UserRepository.GetAll();
        }

        public Task Login(UserLoginVm userLoginVm)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(UserRegistrationVm userRegisterVm)
        {
            User user = null;
            bool accountExists = await AccountExists(userRegisterVm.Email);
            if (!accountExists)
            {
                var newUser = new User
                {
                    AvailableSpotsOnBookshelf = Constants.InitialNoSpotsOnBookshelf,
                    Email = userRegisterVm.Email,
                    FirstName = userRegisterVm.FirstName,
                    LastName = userRegisterVm.LastName,
                    NumberOfCoins = 0,
                    Password = userRegisterVm.Password   //TODO password should be encrypted
                };
                user = await unitOfWork.UserRepository.Add(newUser);
            }
            return user;
        }

        private async Task<bool> AccountExists(string email)
        {
            var accountExists = await unitOfWork.UserRepository.Exists(x => x.Email.Equals(email));
            return accountExists;
        }
    }
}
