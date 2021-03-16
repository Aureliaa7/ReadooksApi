using Readooks.BusinessLogicLayer.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task Login(UserLoginDto userLoginDto);
        Task<UserDto> Register(UserRegistrationDto userRegisterDto);
        Task<IEnumerable<UserDto>> GetAll();
    }
}
