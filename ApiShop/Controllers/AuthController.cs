using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Recovery;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly JwtOptions _jwtOptions;

        public AuthController(UserManager<User> userManager, IJwtService jwtService, IEmailService emailService, JwtOptions jwtOptions)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Email))
                return BadRequest("Email is required");

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("User not found");

            var token = _jwtService.GeneratePasswordResetToken(user);

            var resetLink = $"https://localhost:7244/reset-password?token={token}";

            await _emailService.SendEmailAsync(user.Email, "Password Reset", $@"
                <p><a href='{resetLink}'>Reset Password</a></p>
            ");

            return Ok($"Password reset link has been sent to your email. Token: {token}");
        }
    }

}
