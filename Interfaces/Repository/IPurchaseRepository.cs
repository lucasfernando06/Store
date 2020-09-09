using Store.Data.Repository;
using Store.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Interfaces.Repository
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> List(User user);
        Task<Purchase> Get(Guid id, User user);
    }
}
