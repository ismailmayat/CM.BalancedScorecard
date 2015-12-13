using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScorecard.Domain.Abstract;
using CM.BalancedScorecard.Domain.Model.Indicators;
using CM.BalancedScorecard.Domain.Model.Users;

namespace CM.BalancedScorecard.Domain.Model.Dashboards
{
    [Table("Dashboards")]
    public class Dashboard : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public Guid ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public User Manager { get; set; }

        public virtual List<Indicator> Indicators { get; set; }

        //public virtual List<Project> Projects { get; set; }
    }
}
