using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Domain.Validations
{
    public class PurchaseValidator : AbstractValidator<Purchase>
    {
        public PurchaseValidator()
        {
            RuleFor(x => x.Title).NotEmpty().OnAnyFailure(m => throw new Exception("A compra necessita de um nome"));
            RuleFor(x => x.Itens).Must(ValidItens).OnAnyFailure(m => throw new Exception("A compra necessita de itens"));
            RuleFor(x => x.Value).Must(ValidValue).OnAnyFailure(m => throw new Exception("Valor inválido"));
            RuleFor(x => x.User).NotEmpty().OnAnyFailure(m => throw new Exception("A compra necessita um usuário"));
        }

        private static bool ValidValue(decimal value)
        {
            return value > 0;
        }

        private static bool ValidItens(IEnumerable<Domain.ItemProduct> itens)
        {
            return itens != null && itens.Any();
        }
    }
}
