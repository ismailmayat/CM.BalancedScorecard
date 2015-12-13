using System;
using System.ComponentModel.DataAnnotations.Schema;
using CM.BalancedScoreboard.Domain.Model.Indicators;

namespace CM.BalancedScoreboard.Domain.Model.Dashboards
{
    [Table("Dashboard_Indicators")]
    public class DashboardIndicators
    {
        public Guid DashboardId { get; set; }

        public Guid IndicatorId { get; set; }

        [ForeignKey("DashboardId")]
        public Dashboard Dashboard { get; set; }

        [ForeignKey("IndicatorId")]
        public Indicator Indicator { get; set; }
    }
}
