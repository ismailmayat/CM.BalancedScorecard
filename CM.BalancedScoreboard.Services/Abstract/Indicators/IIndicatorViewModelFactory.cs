using CM.BalancedScoreboard.Domain.Model.Indicators;
using CM.BalancedScoreboard.Services.ViewModel.Indicators;

namespace CM.BalancedScoreboard.Services.Abstract.Indicators
{
    public interface IIndicatorViewModelFactory
    {
        IndicatorDetailsViewModel CreateIndicatorDetailsViewModel(Indicator indicator);

        IndicatorMeasureDetailsViewModel CreateMeasureDetailsViewModel(Indicator indicator);
    }
}
