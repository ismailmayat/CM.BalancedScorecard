using System;
using System.Collections.Generic;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorViewModel : IViewModel
    {
        public Guid? Id { get; set; }

        [DisplayName("Name")]
        [StringLength(30)]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(100)]
        public string Description { get; set; }

        [DisplayName("Code")]
        [StringLength(6)]
        public string Code { get; set; }

        [DisplayName("Unit")]
        [StringLength(15)]
        public string Unit { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }

        [DisplayName("Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("Comparison type")]
        public ComparisonValueType ComparisonValueType { get; set; }

        [DisplayName("Periodicity")]
        public PeriodicityType PeriodicityType { get; set; }

        [DisplayName("Value type")]
        public ObjectValueType ObjectValueType { get; set; }

        public string LastRealValue { get; set; }

        public string LastTargetValue { get; set; }

        public DateTime LastMeasureDate { get; set; }

        public string ManagerName { get; set; }

        public Guid IndicatorTypeId { get; set; }

        [DisplayName("Manager")]
        public Guid ManagerId { get; set; }

        [DisplayName("Fulfillment rate")]
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

        public Dictionary<string,string> DisplayNames
        {
            get
            {
                return DataAnnotationsUtils.GetDisplayNameAttributeValue<IndicatorViewModel>();
            }
        }
    }
}
