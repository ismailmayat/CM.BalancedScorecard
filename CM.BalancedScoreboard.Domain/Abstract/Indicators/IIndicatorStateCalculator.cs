using System;
using CM.BalancedScoreboard.Domain.Model.Enums;

namespace CM.BalancedScoreboard.Domain.Abstract.Indicators
{
    public interface IIndicatorStateCalculator
    {
        State Calculate(DateTime lastMeasureDate, string lastRealValue, string lastTargetValue,
            PeriodicityType periodicity, ComparisonValueType comparisonValueType, ObjectValueType objectValueType, int? fullfilmentRate);
    }
}
