using System;
using System.Threading.Tasks;

namespace Store.Data.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetById(Guid id);
        Task AddAsync(T entity);       
        Task DeleteAsync(T entity);        
        Task UpdateAsync(T entity);
    }
}
