using Readooks.BusinessLogicLayer.ViewModels;
using Readooks.DataAccessLayer.DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task Login(UserLoginVm userLoginVm);
        Task<User> Register(UserRegistrationVm userRegisterVm);
        Task<IEnumerable<User>> GetAll();
    }
}
