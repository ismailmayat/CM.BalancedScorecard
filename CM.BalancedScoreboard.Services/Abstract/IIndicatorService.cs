using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Services.ViewModel;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface IIndicatorService
    {
        IList<IndicatorViewModel> GetIndicators(string filter);

        IndicatorViewModel GetIndicator(Guid id);
    }

}
