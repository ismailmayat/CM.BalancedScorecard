using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorMeasureViewModel : IViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        public object RealValue { get; set; }

        [Required]
        public object TargetValue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Guid? IndicatorId { get; set; }

        public string ValueInputType { get; set; }
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
