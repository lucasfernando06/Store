using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Repository;
using Store.Domain;
using Store.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(StoreContext context) : base(context) { }

        public async Task<IEnumerable<Purchase>> List(User user)
        {
            return await _context.Purchases.AsNoTracking().Where(p => p.User == user)
                .Include(p => p.User)
                .Include(p => p.Itens)
                .ThenInclude(i => i.Product).ToListAsync();
        }

        public async Task<Purchase> Get(Guid id, User user)
        {
            return await _context.Purchases.AsNoTracking().Where(p => p.Id == id && p.User == user).FirstOrDefaultAsync(); 
        }
    }
}
