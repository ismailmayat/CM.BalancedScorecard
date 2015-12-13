using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CM.BalancedScorecard.Data.Repository.Abstract
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void SetState<TEntity>(TEntity entity, EntityState state) where TEntity : class;

        void SetValues(object oldEntity, object newEntity);

        int SaveChanges();

        void Dispose();
    }
}