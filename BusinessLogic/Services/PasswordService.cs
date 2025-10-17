using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
namespace BusinessLogic.Services
{
    public class PasswordService
    {
        private readonly UserManager<User> userManager;
        private readonly IJwtService jwtService;
        private readonly EmailService emailService;

        public PasswordService(UserManager<User> userManager, IJwtService jwtService, EmailService emailService)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
            this.emailService = emailService;
        }

        public async Task RequestPasswordResetAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found");

            var token = jwtService.GeneratePasswordResetToken(user);

            var resetUrl = $"https://yourfrontend.com/reset-password?token={token}";

            await emailService.SendEmailAsync(user.Email, "Password Reset", $@"
                <p>Click below to reset your password:</p>
                <a href='{resetUrl}'>Reset Password</a>
                <p>This link will expire in 15 minutes.</p>
            ");
        }

    }
}
