using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Abstract;
using CM.BalancedScoreboard.Domain.Model.Dashboards;
using CM.BalancedScoreboard.Domain.Model.Enums;
using CM.BalancedScoreboard.Domain.Model.Objetives;
using CM.BalancedScoreboard.Domain.Model.Users;

namespace CM.BalancedScoreboard.Domain.Model.Indicators
{
    [Table("Indicators")]
    public class Indicator : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public TargetType TargetType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public Periodicity Periodicity { get; set; }

        public bool Splitted { get; set; }

        public SplitType SplitType { get; set; }

        [Required]
        public Guid IndicatorTypeId { get; set; }

        [ForeignKey("IndicatorTypeId")]
        public IndicatorType Type { get; set; }

        [Required]
        public Guid ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public User Manager { get; set; }

        public virtual List<Dashboard> Dashboards { get; set; }
        public virtual List<Objective> Objectives { get; set; }
        public virtual List<IndicatorValue> Values { get; set; }
        public virtual List<IndicatorSplit> Splits { get; set; }
    }
}
