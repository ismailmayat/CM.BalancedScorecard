using CM.BalancedScoreboard.Domain.Abstract;
using CM.BalancedScoreboard.Domain.Model.Dashboards;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Objetives;
using CM.BalancedScoreboard.Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.BalancedScoreboard.Domain.Model.Indicators
{
    [Table("Indicators")]
    public class Indicator : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [MaxLength(6)]
        public string Code { get; set; }

        [Required]
        [MaxLength(15)]
        public string Unit { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public ComparisonValueType ComparisonValueType { get; set; }

        [Required]
        public PeriodicityType PeriodicityType { get; set; }

        [Required]
        public ObjectValueType ObjectValueType { get; set; }

        [Required]
        public Guid IndicatorTypeId { get; set; }

        [ForeignKey("IndicatorTypeId")]
        public IndicatorType Type { get; set; }

        [Required]
        public Guid ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public User Manager { get; set; }

        public int? FulfillmentRate { get; set; }

        public virtual List<Dashboard> Dashboards { get; set; }
        public virtual List<Objective> Objectives { get; set; }
        public virtual List<IndicatorMeasure> Measures { get; set; }

        public Indicator()
        {
            Measures = new List<IndicatorMeasure>();
        }
    }
}
