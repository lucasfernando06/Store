using Store.Models.Purchase;

namespace Store.Models.Product
{
    public class PurchaseModel
    {
        public string Name { get; set; }       
        public PurchaseProductModel[] Products { get; set; }             
    }
}
