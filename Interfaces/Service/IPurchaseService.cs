using Store.Models.Product;
using Store.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Interfaces.Service
{
    public interface IPurchaseService
    {
        Task Insert(PurchaseModel model);
        Task<IEnumerable<PurchaseListModel>> List();
        Task<PurchaseListModel> Get(Guid id);
    }
}
