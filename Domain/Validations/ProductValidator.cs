using FluentValidation;
using System;

namespace Store.Domain.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().OnAnyFailure(m => throw new Exception("O produto necessita de um nome"));              
            RuleFor(x => x.Description).NotEmpty().OnAnyFailure(m => throw new Exception("O produto necessita de uma descrição"));
            RuleFor(x => x.Price).Must(ValidValue).OnAnyFailure(m => throw new Exception("Preço inválido"));          
        }

        private static bool ValidValue(decimal value)
        {
            return value > 0;
        }
    }
}
