using System;
using System.Threading.Tasks;

namespace Store.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {        
        Task CommitAsync();
    }
}
