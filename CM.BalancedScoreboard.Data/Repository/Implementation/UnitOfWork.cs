using System;
using CM.BalancedScoreboard.Data.Repository.Abstract;

namespace CM.BalancedScoreboard.Data.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly IDbContext _context;

        public UnitOfWork(IDbContext ctx) 
        {
            _context = ctx;
        }

        public IDbContext Context => _context;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}
