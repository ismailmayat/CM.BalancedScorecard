using System.Collections.Generic;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorListViewModel : IListViewModel
    {
        public IEnumerable<IViewModel> List { get; set; }
    }
}
