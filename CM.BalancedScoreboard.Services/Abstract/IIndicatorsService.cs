using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface IIndicatorsService
    {
        IList<IndicatorViewModel> GetIndicators(string filter);

        IndicatorDetailsViewModel GetIndicator(Guid id);

        void Add(IndicatorViewModel indicatorVm);

        void Update(IndicatorViewModel indicatorVm);

        void Delete(Guid id);

        IList<IndicatorMeasureViewModel> GetMeasures(Guid indicatorId);

        bool AddMeasure(IndicatorMeasureViewModel indicatorMeasureVm);

        bool UpdateMeasure(IndicatorMeasureViewModel indicatorMeasureVm);

        bool DeleteMeasure(Guid indicatorId, Guid indicatorMeasureId);
    }
}
