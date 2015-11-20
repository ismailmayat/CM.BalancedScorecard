using CM.BalancedScoreboard.Domain.Model.Indicators;
using System;

namespace CM.BalancedScoreboard.Data.Repository.Abstract.Indicators
{
    public interface IIndicatorsRepository : IBaseRepository<Indicator>
    {
        bool AddMeasure(IndicatorMeasure indicatorMeasure);

        bool UpdateMeasure(IndicatorMeasure indicatorMeasure);

        bool DeleteMeasure(Guid indicatorId, Guid indicatorMeasureId);
    }
}
