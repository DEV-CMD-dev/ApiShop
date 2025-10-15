using BusinessLogic.DTOs.Account;
namespace BusinessLogic.Interfaces
{
    public interface IAccountsService
    {
        Task Register(RegisterModel model);
        Task<LoginResponse> Login(LoginModel model);
        Task Logout(LogoutModel model);
    }
}
