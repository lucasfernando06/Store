using Store.Models.User;
using System.Threading.Tasks;

namespace Store.Interfaces.Service
{
    public interface IUserService
    {
        Task InsertValidations(UserModel model);
        Task Insert(UserModel model);
    }
}
