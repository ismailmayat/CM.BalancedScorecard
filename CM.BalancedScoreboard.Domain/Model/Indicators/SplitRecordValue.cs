using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.BalancedScoreboard.Domain.Model.Indicators
{
    [Table("Indicator_Split_RecordValues")]
    public class SplitRecordValue
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string RecordValue { get; set; }

        [Required]
        public string TargetValue { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid IndicatorSplitId { get; set; }

        [ForeignKey("IndicatorSplitId")]
        public IndicatorSplit Indicator { get; set; }
    }
}
