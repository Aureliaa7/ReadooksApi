using Microsoft.AspNetCore.Mvc;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.Dtos;
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
            var createdUser = await accountService.Register(user);
            if (createdUser != null)
            {
                return Created(Url.Action("Register"), createdUser);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var user = await accountService.Login(model);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await accountService.GetAll();
            return Ok(users);
        }
    }
}
