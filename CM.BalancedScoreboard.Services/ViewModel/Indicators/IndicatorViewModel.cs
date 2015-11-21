using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Services.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CM.BalancedScoreboard.Services.Utils;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorViewModel : IViewModel
    {
        public Guid? Id { get; set; }

        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [StringLength(30)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }

        [Display(Name = "Code")]
        [DataType(DataType.Text)]
        [StringLength(6)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public string Code { get; set; }

        [Display(Name = "Unit")]
        [DataType(DataType.Text)]
        [StringLength(15)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public string Unit { get; set; }

        [Display(Name = "Active")]
        [CustomDataType(CDataType.YesNo)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public bool Active { get; set; }

        [Display(Name = "StartDate")]
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public DateTime StartDate { get; set; }

        [Display(Name = "ComparisonValueType")]
        [EnumDataType(typeof(ComparisonValueType))]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public ComparisonValueType ComparisonValueType { get; set; }

        [Display(Name = "PeriodicityType")]
        [EnumDataType(typeof(PeriodicityType))]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public PeriodicityType PeriodicityType { get; set; }

        [Display(Name = "ObjectValueType")]
        [EnumDataType(typeof(ObjectValueType))]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public ObjectValueType ObjectValueType { get; set; }

        [Display(Name = "IndicatorType")]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public Guid IndicatorTypeId { get; set; }

        [Display(Name = "Manager")]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public Guid ManagerId { get; set; }

        [Display(Name = "FulfillmentRate")]
        [CustomDataType(CDataType.Range)]
        [Range(50, 100)]
        public int? FulfillmentRate { get; set; }

        public string LastRealValue { get; set; }

        public string LastTargetValue { get; set; }

        public DateTime? LastMeasureDate { get; set; }

        public string ManagerName { get; set; }

        public State State { get; set; }
    }

    public class IndicatorDetailsViewModel
    {
        public IndicatorViewModel Data { get; set; }

        public Dictionary<string, Dictionary<string, object>> Config { get; set; }

        public List<Option> IndicatorTypes { get; set; }

        public List<Option> Users { get; set; }
    }

    public class IndicatorListViewModel
    {
        public List<IndicatorViewModel> Data { get; set; }
    }
}
