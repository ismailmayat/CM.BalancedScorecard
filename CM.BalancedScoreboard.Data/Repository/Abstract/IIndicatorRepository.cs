using System;
using CM.BalancedScoreboard.Domain.Model.Indicators;

namespace CM.BalancedScoreboard.Data.Repository.Abstract
{
    public interface IIndicatorRepository : IBaseRepository<Indicator>
    {
        bool AddMeasure(IndicatorMeasure indicatorMeasure);

        bool UpdateMeasure(IndicatorMeasure indicatorMeasure);

        bool DeleteMeasure(Guid indicatorId, Guid indicatorMeasureId);
    }
}
