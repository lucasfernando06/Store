using Microsoft.Extensions.Logging;
using Store.Data.Context;
using System;
using System.Threading.Tasks;

namespace Store.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StoreContext _context;        
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(StoreContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;         
            _logger = logger;
        }       

        public async Task CommitAsync()
        {           
            var result = await _context.SaveChangesAsync();
            _logger.LogTrace("Commited...", result);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
