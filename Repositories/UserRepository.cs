using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Repository;
using Store.Domain;
using Store.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(StoreContext context) : base(context) { }

        public async Task<User> GetAuthUser(string user, string password)
        {
            return await _context.Users.AsNoTracking().Where(u => u.Password.ToLower() == password.ToLower() && 
                (u.Email.ToLower() == user.ToLower() || u.Cpf.ToLower() == user.ToLower()))
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.AsNoTracking().Where(u => u.Email.ToLower() == email.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByCpf(string cpf)
        {
            return await _context.Users.AsNoTracking().Where(u => u.Cpf.ToLower() == cpf.ToLower())
                .FirstOrDefaultAsync();
        }
    }
}
