using System;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorValueViewModel : IViewModel
    {
        public Guid Id { get; set; }

        public string RecordValue { get; set; }

        public string TargetValue { get; set; }

        public DateTime Date { get; set; }

        public Guid IndicatorId { get; set; }
    }
}
