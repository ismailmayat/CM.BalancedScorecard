using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Services.ViewModel.Indicators
{
    public class IndicatorMeasureViewModel : IViewModel
    {
        public Guid? Id { get; set; }

        [StringLength(6)]
        public string RealValue { get; set; }

        [StringLength(6)]
        public string TargetValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Guid? IndicatorId { get; set; }
    }

    public class IndicatorMeasureListViewModel
    {
        public int Year { get; set; }
        public List<IndicatorMeasureViewModel> Measures { get; set; }
    }
}
