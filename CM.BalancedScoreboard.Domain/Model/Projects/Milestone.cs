using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Domain.Model.Projects
{
    [Table("Project_Milestones")]
    public class Milestone : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public decimal? ProgressPercentage { get; set; }

        [Required]
        public int State { get; set; }

        public string Comment { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}
