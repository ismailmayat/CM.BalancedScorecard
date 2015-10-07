using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Domain.Model.Indicators
{
    [Table("Indicator_Splits")]
    public class IndicatorSplit : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid IndicatorId { get; set; }

        [ForeignKey("IndicatorId")]
        public Indicator Indicator { get; set; }

        public List<SplitRecordValue> RecordValues { get; set; }
    }
}
