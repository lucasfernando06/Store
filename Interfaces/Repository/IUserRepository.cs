using Store.Data.Repository;
using Store.Domain;
using System.Threading.Tasks;

namespace Store.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAuthUser(string user, string password);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByCpf(string cpf);
    }
}
