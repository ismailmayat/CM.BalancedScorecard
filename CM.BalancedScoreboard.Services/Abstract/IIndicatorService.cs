using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Services.ViewModel;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface IIndicatorService
    {
        IEnumerable<IndicatorViewModel> GetIndicators(string filter);

        IndicatorViewModel GetIndicator(Guid id);
    }

}
