using System;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Indicators;

namespace CM.BalancedScoreboard.Services.Dto
{
    public class IndicatorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public TargetType TargetType { get; set; }
        public IndicatorState? State => CalculateState();
        public IndicatorValue LastValue { get; set; }

        private IndicatorState? CalculateState()
        {
            if (LastValue == null)
                return null;

            switch (TargetType)
            {
                case TargetType.Equal:
                    return LastValue.RecordValue == LastValue.TargetValue ? IndicatorState.Green : IndicatorState.Red;
                case TargetType.Greater:
                    return LastValue.RecordValue > LastValue.TargetValue ? IndicatorState.Green : IndicatorState.Red;
                case TargetType.Smaller:
            }
        }

        private GetConvertedValue<T>(string value)
        {
            
        }
    }
}
