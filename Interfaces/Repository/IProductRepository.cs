using Store.Data.Repository;
using Store.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> List();
    }   
}
