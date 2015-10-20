using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Domain.Model.Enums;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorViewModel
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

        public bool Splitted { get; set; }

        public SplitType SplitType { get; set; }

        public Guid IndicatorTypeId { get; set; }

        public Guid ManagerId { get; set; }

        public List<IndicatorValueViewModel> Values { get; set; }

        public IndicatorState? State => CalculateState(LastRecordValue, LastTargetValue, ObjectValueType, ComparisonValueType);

        private IndicatorState? CalculateState(string lastRecordValue, string lastTargetValue, ObjectValueType ObjectValueType, ComparisonValueType ComparisonValueType)
        {
            if (string.IsNullOrEmpty(lastRecordValue) || string.IsNullOrEmpty(lastTargetValue))
                return null;

            switch (ObjectValueType)
            {
                case ObjectValueType.Integer:
                    return GetIntegerBasedState(lastRecordValue, lastTargetValue, ComparisonValueType);
                case ObjectValueType.Decimal:
                    return GetDecimalBasedState(lastRecordValue, lastTargetValue, ComparisonValueType);
                case ObjectValueType.Boolean:
                    return GetBoolBasedState(lastRecordValue, lastTargetValue, ComparisonValueType);
                default:
                    return null;
            }
        }

        private IndicatorState? GetIntegerBasedState(string lastRecordValue, string lastTargetValue, ComparisonValueType ComparisonValueType)
        {
            int recordValue;
            int targetValue;
            int.TryParse(lastRecordValue, out recordValue);
            int.TryParse(lastTargetValue, out targetValue);

            switch (ComparisonValueType)
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

        private IndicatorState? GetDecimalBasedState(string lastRecordValue, string lastTargetValue, ComparisonValueType ComparisonValueType)
        {
            decimal recordValue;
            decimal targetValue;
            decimal.TryParse(lastRecordValue, out recordValue);
            decimal.TryParse(lastTargetValue, out targetValue);

            switch (ComparisonValueType)
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

        private IndicatorState? GetBoolBasedState(string lastRecordValue, string lastTargetValue, ComparisonValueType ComparisonValueType)
        {
            bool recordValue;
            bool targetValue;
            bool.TryParse(lastRecordValue, out recordValue);
            bool.TryParse(lastTargetValue, out targetValue);

            switch (ComparisonValueType)
            {
                case ComparisonValueType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }
    }

    public class IndicatorValueViewModel
    {
        public Guid Id { get; set; }

        public string RecordValue { get; set; }

        public string TargetValue { get; set; }

        public DateTime Date { get; set; }

        public Guid IndicatorId { get; set; }
    }
}
