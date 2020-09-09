using Store.Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Interfaces.Service
{
    public interface IProductService
    {
        Task Insert(ProductModel model);
        Task<IEnumerable<ProductListModel>> List();
        Task<ProductListModel> Get(Guid id);
        Task<ProductListModel> Update(EditProductModel editModel);
    }
}
