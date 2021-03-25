using Microsoft.AspNetCore.Mvc;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.Dtos.Users;
using System.Threading.Tasks;

namespace ReadooksApi.Controllers
{
    public class AccountsController : ReadooksController
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationDto user)
        {
            var createdUser = await accountService.RegisterAsync(user);
            if (createdUser != null)
            {
                return Created(Url.Action("Register"), createdUser);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var user = await accountService.LoginAsync(model);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }
}
