using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.Dto;

namespace CM.BalancedScoreboard.Services.ViewModel
{
    public class IndicatorViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Unit { get; set; }

        public bool Active { get; set; }

        public ValueComparisonType ValueComparisonType { get; set; }

        public DateTime StartDate { get; set; }

        public PeriodicityType PeriodicityType { get; set; }

        public ValueObjectType ValueObjectType { get; set; }

        public bool Splitted { get; set; }

        public SplitType SplitType { get; set; }

        public Guid IndicatorTypeId { get; set; }

        public Guid ManagerId { get; set; }

        public string ManagerName { get; set; }

        public string LastRecordValue { get; set; }

        public string LastTargetValue { get; set; }

        public IndicatorState? State => CalculateState(LastRecordValue, LastTargetValue, ValueObjectType, ValueComparisonType);

        public virtual List<IndicatorValueViewModel> Values { get; set; }

        private IndicatorState? CalculateState(string lastRecordValue, string lastTargetValue, ValueObjectType valueObjectType, ValueComparisonType valueComparisonType)
        {
            if (string.IsNullOrEmpty(lastRecordValue) || string.IsNullOrEmpty(lastTargetValue))
                return null;

            switch (valueObjectType)
            {
                case ValueObjectType.Integer:
                    return GetIntegerBasedState(lastRecordValue, lastTargetValue, valueComparisonType);
                case ValueObjectType.Decimal:
                    return GetDecimalBasedState(lastRecordValue, lastTargetValue, valueComparisonType);
                case ValueObjectType.Boolean:
                    return GetBoolBasedState(lastRecordValue, lastTargetValue, valueComparisonType);
                default:
                    return null;
            }
        }

        private IndicatorState? GetIntegerBasedState(string lastRecordValue, string lastTargetValue, ValueComparisonType valueComparisonType)
        {
            int recordValue;
            int targetValue;
            int.TryParse(lastRecordValue, out recordValue);
            int.TryParse(lastTargetValue, out targetValue);

            switch (valueComparisonType)
            {
                case ValueComparisonType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                case ValueComparisonType.Greater:
                    return recordValue > targetValue ? IndicatorState.Green : IndicatorState.Red;
                case ValueComparisonType.Smaller:
                    return recordValue < targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }

        private IndicatorState? GetDecimalBasedState(string lastRecordValue, string lastTargetValue, ValueComparisonType valueComparisonType)
        {
            decimal recordValue;
            decimal targetValue;
            decimal.TryParse(lastRecordValue, out recordValue);
            decimal.TryParse(lastTargetValue, out targetValue);

            switch (valueComparisonType)
            {
                case ValueComparisonType.Equal:
                    return recordValue == targetValue ? IndicatorState.Green : IndicatorState.Red;
                case ValueComparisonType.Greater:
                    return recordValue > targetValue ? IndicatorState.Green : IndicatorState.Red;
                case ValueComparisonType.Smaller:
                    return recordValue < targetValue ? IndicatorState.Green : IndicatorState.Red;
                default:
                    return null;
            }
        }

        private IndicatorState? GetBoolBasedState(string lastRecordValue, string lastTargetValue, ValueComparisonType valueComparisonType)
        {
            bool recordValue;
            bool targetValue;
            bool.TryParse(lastRecordValue, out recordValue);
            bool.TryParse(lastTargetValue, out targetValue);

            switch (valueComparisonType)
            {
                case ValueComparisonType.Equal:
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
