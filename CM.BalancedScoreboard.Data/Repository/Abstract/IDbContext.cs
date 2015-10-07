using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CM.BalancedScoreboard.Data.Repository.Abstract
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void SetModified<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        void Dispose();
    }
}