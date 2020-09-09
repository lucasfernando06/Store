using Store.Data.Context;
using Store.Data.Repository;
using Store.Domain;
using Store.Interfaces.Repository;

namespace Store.Repositories
{ 
    public class ItemProductRepository : Repository<ItemProduct>, IItemProductRepository
    {
        public ItemProductRepository(StoreContext context) : base(context) { }
    }
}
