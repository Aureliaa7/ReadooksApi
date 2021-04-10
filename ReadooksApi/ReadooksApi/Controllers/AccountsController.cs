using Microsoft.AspNetCore.Mvc;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.BusinessLogicLayer.Dtos.Users;
using System.Threading.Tasks;
using System;
using Readooks.BusinessLogicLayer.Exceptions;

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

        [HttpGet("info/{readerId}")]
        public async Task<IActionResult> GetAccountInfo(Guid readerId)
        {
            try
            {
                var accountInfo = await accountService.GetInfo(readerId);
                return Ok(accountInfo);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await accountService.GetByIdAsync(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/{noCoins}")]
        public async Task<IActionResult> UpdateNoCoins(Guid id, int noCoins)
        {
            try
            {
                return Ok(await accountService.UpdateNoCoinsAsync(id, noCoins));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPut("buy-spot/{id}/{noCoins}")]
        public async Task<IActionResult> BuySpotOnBookshelf(Guid id, int noCoins)
        {
            try
            {
                var userDto = await accountService.BuySpotOnBookshelfAsync(id, noCoins);
                if (userDto != null)
                {
                    return Ok(userDto);
                }
                return BadRequest();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("free-spot/{id}/{noOfSpotsOnBookshelf}")]
        public async Task<IActionResult> FreeSpotOnBookshelf(Guid id, int noOfSpotsOnBookshelf)
        {
            try
            {
                var userDto = await accountService.UpdateNoSpotsOnBookshelfAsync(id, noOfSpotsOnBookshelf);
                if (userDto != null)
                {
                    return Ok(userDto);
                }
                return BadRequest();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
