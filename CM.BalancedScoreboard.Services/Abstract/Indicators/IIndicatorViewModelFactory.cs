using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;
using System.Linq;
using CM.BalancedScoreboard.Domain.Model.Users;

namespace CM.BalancedScoreboard.Services.Abstract.Indicators
{
    public interface IIndicatorViewModelFactory
    {
        IndicatorDetailsViewModel CreateIndicatorDetailsViewModel(Indicator indicator, IQueryable<IndicatorType> indicatorTypes, IQueryable<User> users);

        IndicatorMeasureDetailsViewModel CreateMeasureDetailsViewModel(Indicator indicator);

        IndicatorListViewModel CreateIndicatorListViewModel(IQueryable<Indicator> indicators);
    }
}
