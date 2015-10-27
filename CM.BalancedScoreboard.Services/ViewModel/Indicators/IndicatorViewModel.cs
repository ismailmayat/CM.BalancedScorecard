using System;
using CM.BalancedScoreboard.Domain.Model.Enums;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorViewModel : IViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public ComparisonValueType ComparisonValueType { get; set; }

        public PeriodicityType PeriodicityType { get; set; }

        public ObjectValueType ObjectValueType { get; set; }

        public string LastRecordValue { get; set; }

        public string LastTargetValue { get; set; }

        public string ManagerName { get; set; }

        public string Code { get; set; }

        public bool Active { get; set; }

        public DateTime StartDate { get; set; }

        public Guid IndicatorTypeId { get; set; }

        public Guid ManagerId { get; set; }

        public IndicatorState? State => CalculateState(this.LastRecordValue, this.LastTargetValue, this.ObjectValueType, this.ComparisonValueType);

        private IndicatorState? CalculateState(string lastRecordValue, string lastTargetValue, ObjectValueType objectValueType, ComparisonValueType comparisonValueType)
        {
            if (string.IsNullOrEmpty(lastRecordValue) || string.IsNullOrEmpty(lastTargetValue))
                return null;

            switch (objectValueType)
            {
                case ObjectValueType.Integer:
                    return GetIntegerBasedState(lastRecordValue, lastTargetValue, comparisonValueType);
                case ObjectValueType.Decimal:
                    return GetDecimalBasedState(lastRecordValue, lastTargetValue, comparisonValueType);
                case ObjectValueType.Boolean:
                    return GetBoolBasedState(lastRecordValue, lastTargetValue, comparisonValueType);
                default:
                    return null;
            }
        }

        private IndicatorState? GetIntegerBasedState(string lastRecordValue, string lastTargetValue, ComparisonValueType comparisonValueType)
        {
            int recordValue;
            int targetValue;
            int.TryParse(lastRecordValue, out recordValue);
            int.TryParse(lastTargetValue, out targetValue);

            switch (comparisonValueType)
            {
                case ComparisonValueType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                case ComparisonValueType.Greater:
                    return recordValue > targetValue ? IndicatorState.Green : IndicatorState.Red;
                case ComparisonValueType.Smaller:
                    return recordValue < targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }

        private IndicatorState? GetDecimalBasedState(string lastRecordValue, string lastTargetValue, ComparisonValueType comparisonValueType)
        {
            decimal recordValue;
            decimal targetValue;
            decimal.TryParse(lastRecordValue, out recordValue);
            decimal.TryParse(lastTargetValue, out targetValue);

            switch (comparisonValueType)
            {
                case ComparisonValueType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                case ComparisonValueType.Greater:
                    return recordValue > targetValue ? IndicatorState.Green : IndicatorState.Red;
                case ComparisonValueType.Smaller:
                    return recordValue < targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }

        private IndicatorState? GetBoolBasedState(string lastRecordValue, string lastTargetValue, ComparisonValueType comparisonValueType)
        {
            bool recordValue;
            bool targetValue;
            bool.TryParse(lastRecordValue, out recordValue);
            bool.TryParse(lastTargetValue, out targetValue);

            switch (comparisonValueType)
            {
                case ComparisonValueType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }
    }
}
