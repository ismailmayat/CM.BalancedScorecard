using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.Utils;
using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorViewModel : IViewModel
    {
        public Guid? Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(6)]
        public string Code { get; set; }

        [StringLength(15)]
        public string Unit { get; set; }

        public bool Active { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public ComparisonValueType ComparisonValueType { get; set; }

        public PeriodicityType PeriodicityType { get; set; }

        public ObjectValueType ObjectValueType { get; set; }

        [StringLength(6)]
        public string LastRealValue { get; set; }

        [StringLength(6)]
        public string LastTargetValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastMeasureDate { get; set; }

        public string ManagerName { get; set; }

        public Guid IndicatorTypeId { get; set; }

        public Guid ManagerId { get; set; }

        [Range(50,100)]
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
