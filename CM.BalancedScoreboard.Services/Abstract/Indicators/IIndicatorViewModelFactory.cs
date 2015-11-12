using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;
using System.Linq;

namespace CM.BalancedScoreboard.Services.Abstract.Indicators
{
    public interface IIndicatorViewModelFactory
    {
        IndicatorDetailsViewModel CreateIndicatorDetailsViewModel(Indicator indicator);

        IndicatorMeasureDetailsViewModel CreateMeasureDetailsViewModel(Indicator indicator);

        IndicatorListViewModel CreateIndicatorListViewModel(IQueryable<Indicator> indicators);
    }
}
