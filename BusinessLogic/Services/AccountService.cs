using AutoMapper;
using BusinessLogic.DTOs.Account;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AccountsService(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public Task Login(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task Logout(LogoutModel model)
        {
            throw new NotImplementedException();
        }

        public async Task Register(RegisterModel model)
        {

            var user = mapper.Map<User>(model);

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new HttpException(result.Errors?.FirstOrDefault()?.Description ?? "Error", HttpStatusCode.BadRequest);
        }


    }
}
