﻿using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using System;
using System.Data.Entity;
using System.Linq;
using CM.BalancedScoreboard.Data.Repository.Abstract.Indicators;

namespace CM.BalancedScoreboard.Data.Repository.Implementation.Indicators
{
    public class IndicatorsRepository : BaseRepository<Indicator>, IIndicatorsRepository
    {
        public IndicatorsRepository(IUnitOfWork uof) : base(uof) { }

        public bool AddMeasure(IndicatorMeasure indicatorMeasure)
        {
            var indicator = Single(i => i.Id == indicatorMeasure.IndicatorId, i => i.Measures);
            if (indicator == null)
                return false;

            indicator.Measures.Add(indicatorMeasure);
            _context.SetState(indicatorMeasure, EntityState.Added);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateMeasure(IndicatorMeasure indicatorMeasure)
        {
            var indicator = Single(i => i.Id == indicatorMeasure.IndicatorId, i => i.Measures);

            var indicatorMeasureDb = indicator?.Measures.FirstOrDefault(im => im.Id == indicatorMeasure.Id);
            if (indicatorMeasure == null)
                return false;

            _context.SetValues(indicatorMeasureDb, indicatorMeasure);
            _context.SetState(indicatorMeasure, EntityState.Modified);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteMeasure(Guid indicatorId, Guid indicatorMeasureId)
        {
            var indicator = Single(i => i.Id == indicatorId, i => i.Measures);

            var indicatorMeasure = indicator?.Measures.FirstOrDefault(im => im.Id == indicatorMeasureId);
            if (indicatorMeasure == null)
                return false;

            _context.SetState(indicatorMeasure, EntityState.Deleted);
            _context.SaveChanges();

            return true;
        }
    }   
}
