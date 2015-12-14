using CM.BalancedScorecard.Domain.Abstract;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CM.BalancedScorecard.Data.Repository.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        TEntity Single(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> filter = null);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(Guid id);
    }
}
