using System;

namespace CM.BalancedScorecard.Data.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContext Context { get; }

        int SaveChanges();
    }
}
