using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Data.Repository.Implementation
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        protected IDbContext _context;

        public BaseRepository() { } 

        public BaseRepository(IUnitOfWork uof)
        {
            _context = uof.Context;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return _context.Set<TEntity>().Where(filter);
            else
                return _context.Set<TEntity>();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var indicators = _context.Set<TEntity>().AsQueryable();

            foreach (var navigationProperty in navigationProperties)
            {
                indicators = indicators.Include(ExpressionBuilder.GetPropertyName(navigationProperty));
            }

            return indicators.SingleOrDefault(filter);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var i = await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
            return i;
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            var entity = Single(e => e.Id == id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
            _context.SaveChanges();
        }

        //protected static System.Data.Entity.EntityState GetEntityState(Domain.Abstract.EntityState entityState)
        //{
        //    switch (entityState)
        //    {
        //        case Domain.Abstract.EntityState.Unchanged:
        //            return System.Data.Entity.EntityState.Unchanged;
        //        case Domain.Abstract.EntityState.Added:
        //            return System.Data.Entity.EntityState.Added;
        //        case Domain.Abstract.EntityState.Modified:
        //            return System.Data.Entity.EntityState.Modified;
        //        case Domain.Abstract.EntityState.Deleted:
        //            return System.Data.Entity.EntityState.Deleted;
        //        default:
        //            return System.Data.Entity.EntityState.Detached;
        //    }
        //}
    }
}
