using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Repository;
using Store.Domain;
using Store.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context) { }     
        
        public async Task<IEnumerable<Product>> List()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }
    }   
}
