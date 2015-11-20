using CM.BalancedScoreboard.Services.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorMeasureViewModel : IViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "RealValue")]
        [CustomDataType(CDataType.Number)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public object RealValue { get; set; }
        
        [Display(Name = "TargetValue")]
        [CustomDataType(CDataType.Number)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public object TargetValue { get; set; }

        [Display(Name = "Date")]
        [CustomDataType(CDataType.Month)]
        [Required(ErrorMessageResourceName = "RequiredField")]
        public DateTime Date { get; set; }

        public Guid? IndicatorId { get; set; }
    }

    public class IndicatorMeasureListViewModel
    {
        public int Year { get; set; }
        public List<IndicatorMeasureViewModel> Measures { get; set; }  
    }

    public class IndicatorMeasureDetailsViewModel
    {
        public List<IndicatorMeasureListViewModel> Data {get;set; }
        public Dictionary<string, Dictionary<string, object>> Config { get; set; }
    }
}
