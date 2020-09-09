using Store.Domain.Abstract;
using System;

namespace Store.Domain
{
    public class ItemProduct : Entity
    {
        public Guid PurchaseId { get; private set; }
        public Purchase Purchase { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Amount { get; private set; }
        public decimal Value { get; private set; }

        protected ItemProduct() { }
       
        public ItemProduct(Purchase purchase, Product product, int amount)
        {        
            Purchase = purchase;       
            Product = product;
            Amount = amount;

            CalculateValue();
        }

        public void CalculateValue()
        {
            Value = 0;

            if (Product != null)
                Value = Product.Price * Amount;         
        }
    }
}
