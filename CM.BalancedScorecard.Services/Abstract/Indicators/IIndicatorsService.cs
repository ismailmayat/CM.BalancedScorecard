using CM.BalancedScorecard.Services.ViewModel.Indicators;
using System;
using System.Collections.Generic;

namespace CM.BalancedScorecard.Services.Abstract.Indicators
{
    public interface IIndicatorsService
    {
        IndicatorListViewModel GetIndicators(string filter);

        IndicatorDetailsViewModel GetIndicator(Guid? id);

        Guid Add(IndicatorViewModel indicatorVm);

        void Update(IndicatorViewModel indicatorVm);

        void Delete(Guid id);

        IndicatorMeasureDetailsViewModel GetMeasures(Guid indicatorId);

        bool AddMeasure(IndicatorMeasureViewModel indicatorMeasureVm);

        bool UpdateMeasure(IndicatorMeasureViewModel indicatorMeasureVm);

        bool DeleteMeasure(Guid indicatorId, Guid indicatorMeasureId);

        Dictionary<string, string> GetResources();
    }
}
