using Store.Data.UnitOfWork;
using Store.Domain;
using Store.Interfaces.Repository;
using Store.Interfaces.Service;
using Store.Models.User;
using System;
using System.Threading.Tasks;

namespace Store.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Insert(UserModel model)
        {
            await InsertValidations(model);
          
            await _userRepository.AddAsync(new User(model));
            await _unitOfWork.CommitAsync();
        }     
        
        public async Task InsertValidations(UserModel model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);

            if (user == null)
                user = await _userRepository.GetUserByCpf(model.Cpf);

            if (user != null)
                throw new Exception("Usuário existente. Tente novamente");
        }
    }
}
