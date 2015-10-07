using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Domain.Model.Indicators
{
    [Table("Indicator_RecordValues")]
    public class RecordValue : IndicatorValue
    {
        [Required]
        public Guid IndicatorId { get; set; }

        [ForeignKey("IndicatorId")]
        public Indicator Indicator { get; set; }
    }
}
