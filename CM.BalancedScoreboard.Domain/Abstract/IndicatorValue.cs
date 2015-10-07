using System;
using System.ComponentModel.DataAnnotations;

namespace CM.BalancedScoreboard.Domain.Abstract
{
    public abstract class IndicatorValue : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}