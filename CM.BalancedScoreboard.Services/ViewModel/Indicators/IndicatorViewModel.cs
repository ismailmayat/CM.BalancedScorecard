using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.Utils;

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

        public string LastRealValue { get; set; }

        public string LastTargetValue { get; set; }

        public DateTime LastMeasureDate { get; set; }

        public string ManagerName { get; set; }

        public string Code { get; set; }

        public bool Active { get; set; }

        public DateTime StartDate { get; set; }

        public Guid IndicatorTypeId { get; set; }

        public Guid ManagerId { get; set; }

        public int? FulfillmentRate { get; set; }

        public State State { get; set; }
    }

    public class IndicatorDetailsViewModel
    {
        public IndicatorViewModel Indicator { get; set; }

        public IEnumerable<object> PeriodicityTypeList => EnumUtil.GetOptions<PeriodicityType>();

        public IEnumerable<object> ComparisonValueTypeList => EnumUtil.GetOptions<ComparisonValueType>();

        public IEnumerable<object> ObjectValueTypeList => EnumUtil.GetOptions<ObjectValueType>();

        public IEnumerable<object> SplitTypeList => EnumUtil.GetOptions<SplitType>();
    }
}
