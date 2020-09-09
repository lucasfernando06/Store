using Microsoft.AspNetCore.Mvc;
using Store.Data.Models;
using Store.Domain.Enum;
using Store.Interfaces.Service;
using System;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        private readonly IAuthService _authService;

        public BaseController(IAuthService authService)
        {
            _authService = authService;
        }

        protected new ResultModel Response(object data)
        {          
            return new ResultModel(data);
        }

        protected new ResultModel Response(object data, string message)
        {
            return new ResultModel(data, message);
        }

        protected void ValidateType(UserType type)
        {
            if (_authService.GetUserType() != type)
                throw new Exception("Sem permissão");
        }             
    }
}
