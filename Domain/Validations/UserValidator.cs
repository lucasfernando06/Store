using FluentValidation;
using System;

namespace Store.Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 100)
                .OnAnyFailure(m => throw new Exception("O nome está incorreto"));
            RuleFor(x => x.Password)
               .NotEmpty()
               .Length(6, 100)
               .OnAnyFailure(m => throw new Exception("A senha está incorreta"));
            RuleFor(x => x.Cpf)
               .NotEmpty()
               .Length(11)
               .OnAnyFailure(m => throw new Exception("O cpf está incorreto"));
            RuleFor(x => x.Email)
               .NotEmpty()
               .EmailAddress()             
               .OnAnyFailure(m => throw new Exception("O email está incorreto"));           
        }    
    }
}
