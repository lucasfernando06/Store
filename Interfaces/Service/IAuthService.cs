using Store.Domain;
using Store.Domain.Enum;
using Store.Models.Auth;
using System;
using System.Threading.Tasks;

namespace Store.Interfaces.Service
{
    public interface IAuthService
    {
        Guid? GetUserId();
        bool IsAuthenticated();
        string GenerateToken(User user);    
        Task<string> Login(LoginModel model);
        UserType? GetUserType();
        Task<User> GetLoggedUser();
    }   
}
