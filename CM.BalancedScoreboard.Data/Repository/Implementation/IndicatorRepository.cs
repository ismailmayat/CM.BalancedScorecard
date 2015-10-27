using System;
using System.Data.Entity;
using System.Linq;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;

namespace CM.BalancedScoreboard.Data.Repository.Implementation
{
    public class IndicatorRepository : BaseRepository<Indicator>, IIndicatorRepository
    {
        public IndicatorRepository(IUnitOfWork uof) : base(uof) { }

        public bool AddMeasure(IndicatorMeasure indicatorMeasure)
        {
            var indicator = Single(i => i.Id == indicatorMeasure.IndicatorId);
            if (indicator == null)
                return false;

            indicator.Measures.Add(indicatorMeasure);
            _context.Entry(indicatorMeasure).State = EntityState.Added;
            _context.SaveChanges();

            return true;
        }

        public bool UpdateMeasure(IndicatorMeasure indicatorMeasure)
        {
            var indicator = Single(i => i.Id == indicatorMeasure.IndicatorId);

            var indicatorMeasureDb = indicator?.Measures.FirstOrDefault(im => im.Id == indicatorMeasure.Id);
            if (indicatorMeasure == null)
                return false;

            _context.Entry(indicatorMeasureDb).CurrentValues.SetValues(indicatorMeasure);
            _context.Entry(indicatorMeasureDb).State = EntityState.Modified;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteMeasure(Guid indicatorId, Guid indicatorMeasureId)
        {
            var indicator = Single(i => i.Id == indicatorId);

            var indicatorMeasure = indicator?.Measures.FirstOrDefault(im => im.Id == indicatorMeasureId);
            if (indicatorMeasure == null)
                return false;

            _context.Entry(indicatorMeasure).State = EntityState.Deleted;
            _context.SaveChanges();

            return true;
        }
    }   
}
