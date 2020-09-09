using Store.Models.ItemProduct;
using Store.Models.User;
using System;
using System.Collections.Generic;

namespace Store.Models.Purchase
{
    public class PurchaseListModel
    {
        public Guid Id { get; set; }
        public UserListModel User { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public IEnumerable<ItemProductListModel> Products { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
