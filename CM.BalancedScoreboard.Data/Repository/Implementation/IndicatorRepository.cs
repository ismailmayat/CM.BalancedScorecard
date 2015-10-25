using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using System.Data.Entity;
using System.Linq;

namespace CM.BalancedScoreboard.Data.Repository.Implementation
{
    public class IndicatorRepository : BaseRepository<Indicator>, IIndicatorRepository
    {
        public IndicatorRepository(IUnitOfWork uof) : base(uof) { }

        public override void Update(Indicator entity)
        {
            _context.Set<Indicator>().Attach(entity);
            _context.SetModified(entity);

            ((DbSet<IndicatorValue>)_context.Set<IndicatorValue>()).RemoveRange(entity.Values.Where(iv => iv.EntityState == Domain.Abstract.EntityState.Deleted));

            _context.SaveChanges();
        }
    }   
}
