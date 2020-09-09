using Microsoft.EntityFrameworkCore.Internal;
using Store.Domain.Abstract;
using Store.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Domain
{
    public class Purchase : Entity
    {     
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<ItemProduct> Itens { get; private set; }
        public decimal Value { get; private set; }

        protected Purchase() { }

        public Purchase(string title, User user)
        {
            Title = title;
            User = user;
        }

        public void Validation()
        {
            Validate(this, new PurchaseValidator());
        }

        public void SetItens(List<ItemProduct> itens)
        {
            Itens = itens;          
        }

        public void CalculateValue()
        {
            Value = 0;

            if (Itens != null && Itens.Any())
                foreach (var item in Itens)
                    Value += item.Value;      
        }
    }
}
