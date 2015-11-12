using System.Collections.Generic;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorListViewModel
    {
        public List<IndicatorViewModel> Data { get; set; }
        public Dictionary<string, string> Resources { get; set; }
    }
}
