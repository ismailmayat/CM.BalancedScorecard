using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Services.Abstract
{
    public interface IIndicatorService : ICrudService<IndicatorListViewModel, IndicatorDetailsViewModel, Indicator>
    { }
}
