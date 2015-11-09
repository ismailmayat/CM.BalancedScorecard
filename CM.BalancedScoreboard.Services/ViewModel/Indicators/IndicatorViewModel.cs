using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CM.BalancedScoreboard.Services.Abstract;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorViewModel : IViewModel
    { 
        public Guid? Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(100)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Code")]
        [StringLength(6)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Unit")]
        [StringLength(15)]
        [Required]
        public string Unit { get; set; }

        [Display(Name = "Active")]
        [Required]
        public bool Active { get; set; }

        [Display(Name = "StartDate")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "ComparisonValueType")]
        [Required]
        public ComparisonValueType ComparisonValueType { get; set; }

        [Display(Name = "PeriodicityType")]
        [Required]
        public PeriodicityType PeriodicityType { get; set; }

        [Display(Name = "ObjectValueType")]
        [Required]
        public ObjectValueType ObjectValueType { get; set; }

        public string LastRealValue { get; set; }

        public string LastTargetValue { get; set; }

        public DateTime LastMeasureDate { get; set; }

        public string ManagerName { get; set; }

        public Guid IndicatorTypeId { get; set; }

        [Display(Name = "Manager")]
        [Required]
        public Guid ManagerId { get; set; }

        [Display(Name = "FulfillmentRate")]
        [Range(50,100)]
        public int? FulfillmentRate { get; set; }

        public State State { get; set; }
    }

    public class IndicatorDetailsViewModel
    {
        public IndicatorViewModel Indicator { get; set; }

        public IEnumerable<object> PeriodicityTypeList { get; set; }

        public IEnumerable<object> ComparisonValueTypeList { get; set; }

        public IEnumerable<object> ObjectValueTypeList { get; set; }

        public Dictionary<string,Dictionary<string, object>> Config { get; set; }
    }
}
