using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface IIndicatorService
    {
        IList<IndicatorViewModel> GetIndicators(string filter);

        IndicatorDetailsViewModel GetIndicator(Guid id);

        void Add(IndicatorViewModel indicatorVm);

        void Update(Guid id, IndicatorViewModel indicatorVm);

        void Delete(Guid id);
    }
}
