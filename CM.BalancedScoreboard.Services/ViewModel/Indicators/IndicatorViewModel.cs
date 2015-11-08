using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorViewModel : IViewModel
    {
        public Guid? Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Resources))]
        [StringLength(100)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Resources.Resources))]
        [StringLength(6)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Unit", ResourceType = typeof(Resources.Resources))]
        [StringLength(15)]
        [Required]
        public string Unit { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Resources))]
        [Required]
        public bool Active { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Resources.Resources))]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "ComparisonValueType", ResourceType = typeof(Resources.Resources))]
        [Required]
        public ComparisonValueType ComparisonValueType { get; set; }

        [Display(Name = "PeriodicityType", ResourceType = typeof(Resources.Resources))]
        [Required]
        public PeriodicityType PeriodicityType { get; set; }

        [Display(Name = "ObjectValueType", ResourceType = typeof(Resources.Resources))]
        [Required]
        public ObjectValueType ObjectValueType { get; set; }

        public string LastRealValue { get; set; }

        public string LastTargetValue { get; set; }

        public DateTime LastMeasureDate { get; set; }

        public string ManagerName { get; set; }

        public Guid IndicatorTypeId { get; set; }

        [Display(Name = "Manager", ResourceType = typeof(Resources.Resources))]
        [Required]
        public Guid ManagerId { get; set; }

        [Display(Name = "FulfillmentRate", ResourceType = typeof(Resources.Resources))]
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

        public Dictionary<string,Dictionary<string, object>> Resources
        {
            get
            {
                return DataAnnotationsUtils.GetObjectAttributes<IndicatorViewModel>();
            }
        }
    }
}
