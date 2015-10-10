using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Data.Repository.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        TEntity Single(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> filter = null);

        void Add(IEnumerable<TEntity> entities);

        void Update(IEnumerable<TEntity> entities);

        void Delete(IEnumerable<TEntity> entities);
    }
}
