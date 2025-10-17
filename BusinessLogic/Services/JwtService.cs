using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BusinessLogic.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly JwtOptions jwtOptions;

        public JwtService(IConfiguration configuration, UserManager<User> userManager, JwtOptions jwtOptions)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.jwtOptions = jwtOptions;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtOptions.LifetimeInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.Birthdate.ToString())

            };
            var roles = userManager.GetRolesAsync(user).Result;

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            return claims;

        }

        public string GeneratePasswordResetToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("purpose", "password_reset")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var creditentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creditentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
