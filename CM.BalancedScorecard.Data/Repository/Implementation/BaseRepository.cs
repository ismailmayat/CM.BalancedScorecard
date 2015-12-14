using CM.BalancedScorecard.Data.Repository.Abstract;
using CM.BalancedScorecard.Domain.Abstract;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CM.BalancedScorecard.Data.Repository.Implementation
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        protected IDbContext _context;

        public BaseRepository() { } 

        public BaseRepository(IUnitOfWork uof)
        {
            _context = uof.Context;
        }

        public IQueryable<TEntity> GetAll()
        {
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
            _context.SetState(entity, EntityState.Modified);
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
    }
}
