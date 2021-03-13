using Microsoft.AspNetCore.Mvc;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.ViewModels;
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
        public async Task<ActionResult> Register(UserRegistrationVm user)
        {
            var createdUser = await accountService.Register(user);
            if (createdUser != null)
            {
                return Created(Url.Action("Register"), createdUser);
            }
            return BadRequest();
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await accountService.GetAll();
            return Ok(users);
        }
    }
}
