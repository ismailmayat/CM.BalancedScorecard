namespace CM.BalancedScoreboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dashboards_Readded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dashboards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ManagerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Dashboard_Indicators",
                c => new
                    {
                        DashboardId = c.Guid(nullable: false),
                        IndicatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DashboardId, t.IndicatorId })
                .ForeignKey("dbo.Dashboards", t => t.DashboardId)
                .ForeignKey("dbo.Indicators", t => t.IndicatorId)
                .Index(t => t.DashboardId)
                .Index(t => t.IndicatorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dashboards", "ManagerId", "dbo.Users");
            DropForeignKey("dbo.Dashboard_Indicators", "IndicatorId", "dbo.Indicators");
            DropForeignKey("dbo.Dashboard_Indicators", "DashboardId", "dbo.Dashboards");
            DropIndex("dbo.Dashboard_Indicators", new[] { "IndicatorId" });
            DropIndex("dbo.Dashboard_Indicators", new[] { "DashboardId" });
            DropIndex("dbo.Dashboards", new[] { "ManagerId" });
            DropTable("dbo.Dashboard_Indicators");
            DropTable("dbo.Dashboards");
        }
    }
}
