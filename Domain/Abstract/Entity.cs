using FluentValidation;
using System;

namespace Store.Domain.Abstract
{
    public abstract class Entity
    {       
        public Guid Id { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public DateTime? ChangeDate { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            RegistrationDate = DateTime.Now;
        }

        public void SetChangeDate()
        {
            ChangeDate = DateTime.Now;
        }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {        
            var result = validator.Validate(model);
            return result.IsValid;           
        }
    }
}
