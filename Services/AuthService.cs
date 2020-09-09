using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Store.Domain;
using Store.Domain.Enum;
using Store.Interfaces.Repository;
using Store.Interfaces.Service;
using Store.Models.Auth;
using Store.Models.JWT;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly Jwt _jwtSettings;

        public AuthService(
            IHttpContextAccessor httpContextAccessory, 
            IUserRepository userRepository, 
            Jwt jwtSettings)
        {
            _httpContextAccessor = httpContextAccessory;
            _jwtSettings = jwtSettings;
            _userRepository = userRepository;
        }

        public async Task<User> GetLoggedUser()
        {
            var id = GetUserId();

            if(id != null)        
                return await _userRepository.GetById(id.Value);

            throw new Exception("Nenhum usuário autenticado");   
        }

        public Guid? GetUserId()
        {
            if (IsAuthenticated())
            {
                var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (id != null)
                    return Guid.Parse(id);
            }

            return null;
        }

        public UserType? GetUserType()
        {
            if (IsAuthenticated())
            {
                var type = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

                if (type != null)
                    return (UserType) Enum.Parse(typeof(UserType), type, true);
            }

            return null;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GenerateToken(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                   new GenericIdentity(user.Id.ToString(), "Login"),
                   new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Type.ToString()),
                   }
               );

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Valid,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(Double.Parse(_jwtSettings.ExpirationMinutes))
            });

            var token = handler.WriteToken(securityToken);

            return token;
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await _userRepository.GetAuthUser(model.User, model.Password);
            if (user != null)
                return GenerateToken(user);

            throw new Exception("Usuário e/ou senha inválidos");
        }
    }
}
