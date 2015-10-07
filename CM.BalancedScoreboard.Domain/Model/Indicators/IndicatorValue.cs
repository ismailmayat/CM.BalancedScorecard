using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Domain.Model.Indicators
{
    [Table("Indicator_Values")]
    public abstract class IndicatorValue : IEntity
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
        public Guid IndicatorId { get; set; }

        [ForeignKey("IndicatorId")]
        public Indicator Indicator { get; set; }
    }
}