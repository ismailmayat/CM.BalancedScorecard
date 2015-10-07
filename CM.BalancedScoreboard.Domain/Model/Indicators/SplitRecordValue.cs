using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Domain.Model.Indicators
{
    [Table("Indicator_Split_RecordValues")]
    public class SplitRecordValue : IndicatorValue
    {
        [Required]
        public Guid IndicatorSplitId { get; set; }

        [ForeignKey("IndicatorSplitId")]
        public IndicatorSplit Indicator { get; set; }
    }
}
