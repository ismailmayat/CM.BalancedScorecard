using System;

namespace CM.BalancedScoreboard.Data.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContext Context { get; }

        int SaveChanges();
    }
}
