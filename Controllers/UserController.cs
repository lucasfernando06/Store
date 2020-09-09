using Microsoft.AspNetCore.Mvc;
using Store.Data.Models;
using Store.Interfaces.Service;
using Store.Models.User;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IAuthService authService) : base(authService)
        {
            _userService = userService;
        }
  
        [HttpPost("/users")]
        public async Task<IResultModel> Insert(UserModel model)
        {
            await _userService.Insert(model);
            return Response("Usuário cadastrado com sucesso!");
        }       
    }
}
