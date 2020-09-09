using Store.Domain.Abstract;
using Store.Domain.Validations;
using Store.Models.Product;
using System.Collections.Generic;

namespace Store.Domain
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; } 
        public int Stock { get; private set; }
        public IEnumerable<ItemProduct> PurchaseItens { get; private set; }

        protected Product() { }

        public Product(ProductModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Price = model.Price;
            Stock = model.Stock;

            Validation();
        }

        public void Validation()
        {
            Validate(this, new ProductValidator());                         
        }

        public void SetName(string name)
        {
            Name = name;
            Validation();
        }

        public void SetDesription(string description)
        {
            Description = description;
            Validation();
        }

        public void SetPrice(decimal price)
        {
            Price = price;
            Validation();
        }

        public void SetStock(int stock)
        {
            Stock = stock;
            Validation();
        }
    }
}
