using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Data.Context;

namespace Store.Data.Repository
{
    public abstract class Repository<TDomain> where TDomain : class
    {
        protected readonly StoreContext _context;

        public Repository(StoreContext context)
        {
            _context = context;
        }

        public virtual async Task<TDomain> GetById(Guid id)
        {
            return await _context.Set<TDomain>().FindAsync(id);
        }

        public virtual async Task AddAsync(TDomain entity)
        {
            await _context.Set<TDomain>().AddAsync(entity);
        }       

        public virtual async Task DeleteAsync(TDomain entity)
        {
            await Task.FromResult(_context.Set<TDomain>().Remove(entity));
        }     

        public async Task UpdateAsync(TDomain entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(_context.Set<TDomain>().Update(entity));
        }
    }
}
