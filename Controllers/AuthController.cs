using Microsoft.AspNetCore.Mvc;
using Store.Data.Models;
using Store.Interfaces.Service;
using Store.Models.Auth;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) : base(authService)
        {
            _authService = authService;
        }

        [HttpPost("/login")]
        public async Task<IResultModel> Login(LoginModel model)
        {
            var token = await _authService.Login(model);
            return Response(new { token });          
        }              
    }
}
