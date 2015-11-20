using CM.BalancedScoreboard.Domain.Abstract;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CM.BalancedScoreboard.Data.Repository.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);

        TEntity Single(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> filter = null);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(Guid id);
    }
}
