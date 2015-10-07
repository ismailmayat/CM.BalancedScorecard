using System.Collections.Generic;
using CM.BalancedScoreboard.Services.Dto;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface IIndicatorService
    {
        IEnumerable<IndicatorDto> GetIndicators(string filter);
    }

}
