using BusinessLogic.DTOs.Account;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            await accountsService.Register(model);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var res = await accountsService.Login(model);

            return Ok(res);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutModel model)
        {
            await accountsService.Logout(model);

            return Ok();
        }
    }
}
