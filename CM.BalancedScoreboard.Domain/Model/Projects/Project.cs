using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Abstract;
//using CM.BalancedScoreboard.Domain.Model.Dashboards;
using CM.BalancedScoreboard.Domain.Model.Users;

namespace CM.BalancedScoreboard.Domain.Model.Projects
{
    [Table("Projects")]
    public class Project : IEntity
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
        public bool Active { get; set; }

        [Required]
        public DateTime PlannedStartDate { get; set; }

        [Required]
        public DateTime PlannedEndDate { get; set; }

        public DateTime? RealStartDate { get; set; }

        public DateTime? RealEndDate { get; set; }

        [Required]
        public decimal PlannedBudget { get; set; }

        public decimal? RealBudget { get; set; }

        [Required]
        public Guid ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public User Manager { get; set; }

        //public virtual List<Dashboard> Dashboards { get; set; }
        public virtual List<Milestone> Milestones { get; set; }
    }
}
