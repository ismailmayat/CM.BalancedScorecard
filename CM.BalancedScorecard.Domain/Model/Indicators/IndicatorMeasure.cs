﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScorecard.Domain.Abstract;

namespace CM.BalancedScorecard.Domain.Model.Indicators
{
    [Table("Indicator_Measures")]
    public class IndicatorMeasure : IChildEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(6)]
        public string RealValue { get; set; }

        [Required]
        [MaxLength(6)]
        public string TargetValue { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid IndicatorId { get; set; }

        [ForeignKey("IndicatorId")]
        public Indicator Indicator { get; set; }
    }
}