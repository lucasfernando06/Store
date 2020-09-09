using Store.Domain.Abstract;
using Store.Domain.Enum;
using Store.Domain.Validations;
using Store.Models.User;
using System.Collections.Generic;

namespace Store.Domain
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Cpf { get; private set; }
        public UserType Type { get; private set; }
        public IEnumerable<Purchase> Purchases { get; private set; }

        protected User() { }

        public User(UserModel model)
        {
            Name = model.Name;
            Email = model.Email;
            Password = model.Password;
            Cpf = model.Cpf;        

            if (!string.IsNullOrEmpty(model.Type))
                Type = (UserType)System.Enum.Parse(typeof(UserType), model.Type, true);
            else
                Type = UserType.Employee;

            Validation();
        }

        public void Validation()
        {
            Validate(this, new UserValidator());
        }        
    }
}
