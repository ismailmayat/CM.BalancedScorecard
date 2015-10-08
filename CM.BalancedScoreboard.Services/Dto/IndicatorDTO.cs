using System;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Indicators;
using ValueType = CM.BalancedScoreboard.Domain.Model.Enums.ValueType;

namespace CM.BalancedScoreboard.Services.Dto
{
    public class IndicatorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public ValueType ValueType { get; set; }
        public TargetType TargetType { get; set; }
        public IndicatorState? State => CalculateState(LastRecordValue, LastTargetValue, ValueType, TargetType);
        public string LastRecordValue { get; set; }
        public string LastTargetValue { get; set; }

        private IndicatorState? CalculateState(string lastRecordValue, string lastTargetValue, ValueType valueType, TargetType targetType)
        {
            if (string.IsNullOrEmpty(lastRecordValue) || string.IsNullOrEmpty(lastTargetValue))
                return null;

            switch (valueType)
            {
                case ValueType.Integer:
                    return GetIntegerBasedState(lastRecordValue, lastTargetValue, targetType);
                case ValueType.Decimal:
                    return GetDecimalBasedState(lastRecordValue, lastTargetValue, targetType);
                case ValueType.Boolean:
                    return GetBoolBasedState(lastRecordValue, lastTargetValue, targetType);
                default:
                    return null;
            }
        }

        private IndicatorState? GetIntegerBasedState(string lastRecordValue, string lastTargetValue, TargetType targetType)
        {
            int recordValue;
            int targetValue;
            int.TryParse(lastRecordValue, out recordValue);
            int.TryParse(lastTargetValue, out targetValue);

            switch (targetType)
            {
                case TargetType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                case TargetType.Greater:
                    return recordValue > targetValue ? IndicatorState.Green : IndicatorState.Red;
                case TargetType.Smaller:
                    return recordValue < targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }

        private IndicatorState? GetDecimalBasedState(string lastRecordValue, string lastTargetValue, TargetType targetType)
        {
            decimal recordValue;
            decimal targetValue;
            decimal.TryParse(lastRecordValue, out recordValue);
            decimal.TryParse(lastTargetValue, out targetValue);

            switch (targetType)
            {
                case TargetType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                case TargetType.Greater:
                    return recordValue > targetValue ? IndicatorState.Green : IndicatorState.Red;
                case TargetType.Smaller:
                    return recordValue < targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }

        private IndicatorState? GetBoolBasedState(string lastRecordValue, string lastTargetValue, TargetType targetType)
        {
            bool recordValue;
            bool targetValue;
            bool.TryParse(lastRecordValue, out recordValue);
            bool.TryParse(lastTargetValue, out targetValue);

            switch (targetType)
            {
                case TargetType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }
    }
}
