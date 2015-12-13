using System;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Model.Projects;

namespace CM.BalancedScoreboard.Domain.Model.Dashboards
{
    [Table("Dashboard_Projects")]
    public class DashboardProjects
    {
        public Guid DashboardId { get; set; }

        public Guid ProjectId { get; set; }

        [ForeignKey("DashboardId")]
        public Dashboard Dashboard { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}
