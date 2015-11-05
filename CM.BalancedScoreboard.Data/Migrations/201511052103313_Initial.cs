namespace CM.BalancedScoreboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                "dbo.Indicators",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 6),
                        Unit = c.String(nullable: false, maxLength: 15),
                        Active = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        ComparisonValueType = c.Int(nullable: false),
                        PeriodicityType = c.Int(nullable: false),
                        ObjectValueType = c.Int(nullable: false),
                        IndicatorTypeId = c.Guid(nullable: false),
                        ManagerId = c.Guid(nullable: false),
                        FulfillmentRate = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ManagerId, cascadeDelete: true)
                .ForeignKey("dbo.Indicator_Types", t => t.IndicatorTypeId, cascadeDelete: true)
                .Index(t => t.IndicatorTypeId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Firstname = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Indicator_Measures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RealValue = c.String(nullable: false, maxLength: 6),
                        TargetValue = c.String(nullable: false, maxLength: 6),
                        Date = c.DateTime(nullable: false),
                        IndicatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indicators", t => t.IndicatorId, cascadeDelete: true)
                .Index(t => t.IndicatorId);
            
            CreateTable(
                "dbo.Objectives",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        ManagerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Indicator_Types",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        PlannedStartDate = c.DateTime(nullable: false),
                        PlannedEndDate = c.DateTime(nullable: false),
                        RealStartDate = c.DateTime(),
                        RealEndDate = c.DateTime(),
                        PlannedBudget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RealBudget = c.Decimal(precision: 18, scale: 2),
                        ManagerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Project_Milestones",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ProgressPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        State = c.Int(nullable: false),
                        Comment = c.String(),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Project_Types",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Objective_Indicators",
                c => new
                    {
                        ObjectiveId = c.Guid(nullable: false),
                        IndicatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ObjectiveId, t.IndicatorId })
                .ForeignKey("dbo.Objectives", t => t.ObjectiveId)
                .ForeignKey("dbo.Indicators", t => t.IndicatorId)
                .Index(t => t.ObjectiveId)
                .Index(t => t.IndicatorId);
            
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
            DropForeignKey("dbo.Project_Milestones", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ManagerId", "dbo.Users");
            DropForeignKey("dbo.Dashboards", "ManagerId", "dbo.Users");
            DropForeignKey("dbo.Dashboard_Indicators", "IndicatorId", "dbo.Indicators");
            DropForeignKey("dbo.Dashboard_Indicators", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Indicators", "IndicatorTypeId", "dbo.Indicator_Types");
            DropForeignKey("dbo.Objectives", "ManagerId", "dbo.Users");
            DropForeignKey("dbo.Objective_Indicators", "IndicatorId", "dbo.Indicators");
            DropForeignKey("dbo.Objective_Indicators", "ObjectiveId", "dbo.Objectives");
            DropForeignKey("dbo.Indicator_Measures", "IndicatorId", "dbo.Indicators");
            DropForeignKey("dbo.Indicators", "ManagerId", "dbo.Users");
            DropIndex("dbo.Dashboard_Indicators", new[] { "IndicatorId" });
            DropIndex("dbo.Dashboard_Indicators", new[] { "DashboardId" });
            DropIndex("dbo.Objective_Indicators", new[] { "IndicatorId" });
            DropIndex("dbo.Objective_Indicators", new[] { "ObjectiveId" });
            DropIndex("dbo.Project_Milestones", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ManagerId" });
            DropIndex("dbo.Objectives", new[] { "ManagerId" });
            DropIndex("dbo.Indicator_Measures", new[] { "IndicatorId" });
            DropIndex("dbo.Indicators", new[] { "ManagerId" });
            DropIndex("dbo.Indicators", new[] { "IndicatorTypeId" });
            DropIndex("dbo.Dashboards", new[] { "ManagerId" });
            DropTable("dbo.Dashboard_Indicators");
            DropTable("dbo.Objective_Indicators");
            DropTable("dbo.Project_Types");
            DropTable("dbo.Project_Milestones");
            DropTable("dbo.Projects");
            DropTable("dbo.Indicator_Types");
            DropTable("dbo.Objectives");
            DropTable("dbo.Indicator_Measures");
            DropTable("dbo.Users");
            DropTable("dbo.Indicators");
            DropTable("dbo.Dashboards");
        }
    }
}
