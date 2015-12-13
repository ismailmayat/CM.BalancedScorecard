using CM.BalancedScorecard.Domain.Model.Indicators;
using CM.BalancedScorecard.Services.ViewModel.Indicators;
using System.Linq;
using CM.BalancedScorecard.Domain.Model.Users;

namespace CM.BalancedScorecard.Services.Abstract.Indicators
{
    public interface IIndicatorViewModelFactory
    {
        IndicatorDetailsViewModel CreateIndicatorDetailsViewModel(Indicator indicator, IQueryable<IndicatorType> indicatorTypes, IQueryable<User> users);

        IndicatorMeasureDetailsViewModel CreateMeasureDetailsViewModel(Indicator indicator);

        IndicatorListViewModel CreateIndicatorListViewModel(IQueryable<Indicator> indicators);
    }
}
