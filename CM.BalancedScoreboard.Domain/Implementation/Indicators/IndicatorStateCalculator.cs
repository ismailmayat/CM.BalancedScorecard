using CM.BalancedScoreboard.Domain.Abstract.Indicators;
using CM.BalancedScoreboard.Domain.Model.Enums;
using System;

namespace CM.BalancedScoreboard.Domain.Implementation.Indicators
{
    public class StateCalculator : IIndicatorStateCalculator
    {
        public State Calculate(DateTime lastMeasureDate, string lastRealValue, string lastTargetValue, PeriodicityType periodicity,
            ComparisonValueType comparisonValueType, ObjectValueType objectValueType, int? fullfilmentRate)
        {
            if (string.IsNullOrEmpty(lastRealValue) || string.IsNullOrEmpty(lastTargetValue))
                return State.Grey;

            if (lastMeasureDate.AddMonths(GetMonthNumberFromPeriodicity(periodicity)) < DateTime.Now)
                return State.Grey;

            var targetValueRate = fullfilmentRate.HasValue ? decimal.Divide(fullfilmentRate.Value, 100) : 1;
            
            switch (objectValueType)
            {
                case ObjectValueType.Integer:
                    return GetIntegerBasedState(lastRealValue, lastTargetValue, comparisonValueType, targetValueRate);
                case ObjectValueType.Decimal:
                    return GetDecimalBasedState(lastRealValue, lastTargetValue, comparisonValueType, targetValueRate);
                case ObjectValueType.Boolean:
                    return GetBoolBasedState(lastRealValue, lastTargetValue, comparisonValueType);
                default:
                    return State.Grey;
            }
        }

        private State GetIntegerBasedState(string lastRealValue, string lastTargetValue, ComparisonValueType comparisonValueType, decimal targetValueRate)
        {
            int realValue;
            int targetValue;
            int.TryParse(lastRealValue, out realValue);
            int.TryParse(lastTargetValue, out targetValue);

            switch (comparisonValueType)
            {
                case ComparisonValueType.Equal:
                    return realValue == targetValue ? State.Green : State.Red;
                case ComparisonValueType.Greater:
                    return realValue > targetValue ? State.Green : (realValue > (targetValue - (targetValue * targetValueRate)) ? State.Yellow : State.Red);
                case ComparisonValueType.Smaller:
                    return realValue < targetValue ? State.Green : (realValue < (targetValue + (targetValue * targetValueRate)) ? State.Yellow : State.Red);
                default:
                    return State.Grey;
            }
        }

        private State GetDecimalBasedState(string lastRealValue, string lastTargetValue, ComparisonValueType comparisonValueType, decimal targetValueRate)
        {
            decimal realValue;
            decimal targetValue;
            decimal.TryParse(lastRealValue, out realValue);
            decimal.TryParse(lastTargetValue, out targetValue);

            switch (comparisonValueType)
            {
                case ComparisonValueType.Equal:
                    return realValue == targetValue ? State.Green : State.Red;
                case ComparisonValueType.Greater:
                    return realValue > targetValue ? State.Green : (realValue > (targetValue - (targetValue * targetValueRate)) ? State.Yellow : State.Red);
                case ComparisonValueType.Smaller:
                    return realValue < targetValue ? State.Green : (realValue < (targetValue + (targetValue * targetValueRate)) ? State.Yellow : State.Red);
                default:
                    return State.Grey;
            }
        }

        private State GetBoolBasedState(string lastRealValue, string lastTargetValue, ComparisonValueType comparisonValueType)
        {
            bool realValue;
            bool targetValue;
            bool.TryParse(lastRealValue, out realValue);
            bool.TryParse(lastTargetValue, out targetValue);

            switch (comparisonValueType)
            {
                case ComparisonValueType.Equal:
                    return realValue == targetValue ? State.Green : State.Red;
                default:
                    return State.Grey;
            }
        }

        private int GetMonthNumberFromPeriodicity(PeriodicityType periodicity)
        {
            switch (periodicity)
            {
                case PeriodicityType.Month:
                    return 1;
                case PeriodicityType.TwoMonth:
                    return 2;
                case PeriodicityType.ThreeMonth:
                    return 3;
                case PeriodicityType.FourMonth:
                    return 4;
                case PeriodicityType.SixMonth:
                    return 6;
                case PeriodicityType.TwelveMonth:
                    return 12;
                default:
                    return 1;
            }
        }
    }
}
