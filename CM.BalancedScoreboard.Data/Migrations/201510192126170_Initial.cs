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
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Unit = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ComparisonValueType = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        PeriodicityType = c.Int(nullable: false),
                        ObjectValueType = c.Int(nullable: false),
                        Splitted = c.Boolean(nullable: false),
                        SplitType = c.Int(nullable: false),
                        IndicatorTypeId = c.Guid(nullable: false),
                        ManagerId = c.Guid(nullable: false),
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
                "dbo.Indicator_Splits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        IndicatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indicators", t => t.IndicatorId, cascadeDelete: true)
                .Index(t => t.IndicatorId);
            
            CreateTable(
                "dbo.Indicator_Split_RecordValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RecordValue = c.String(nullable: false),
                        TargetValue = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IndicatorSplitId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indicator_Splits", t => t.IndicatorSplitId, cascadeDelete: true)
                .Index(t => t.IndicatorSplitId);
            
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
                "dbo.Indicator_Values",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RecordValue = c.String(nullable: false),
                        TargetValue = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IndicatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indicators", t => t.IndicatorId, cascadeDelete: true)
                .Index(t => t.IndicatorId);
            
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
            DropForeignKey("dbo.Indicator_Values", "IndicatorId", "dbo.Indicators");
            DropForeignKey("dbo.Indicators", "IndicatorTypeId", "dbo.Indicator_Types");
            DropForeignKey("dbo.Indicator_Split_RecordValues", "IndicatorSplitId", "dbo.Indicator_Splits");
            DropForeignKey("dbo.Indicator_Splits", "IndicatorId", "dbo.Indicators");
            DropForeignKey("dbo.Objectives", "ManagerId", "dbo.Users");
            DropForeignKey("dbo.Objective_Indicators", "IndicatorId", "dbo.Indicators");
            DropForeignKey("dbo.Objective_Indicators", "ObjectiveId", "dbo.Objectives");
            DropForeignKey("dbo.Indicators", "ManagerId", "dbo.Users");
            DropIndex("dbo.Dashboard_Indicators", new[] { "IndicatorId" });
            DropIndex("dbo.Dashboard_Indicators", new[] { "DashboardId" });
            DropIndex("dbo.Objective_Indicators", new[] { "IndicatorId" });
            DropIndex("dbo.Objective_Indicators", new[] { "ObjectiveId" });
            DropIndex("dbo.Project_Milestones", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ManagerId" });
            DropIndex("dbo.Indicator_Values", new[] { "IndicatorId" });
            DropIndex("dbo.Indicator_Split_RecordValues", new[] { "IndicatorSplitId" });
            DropIndex("dbo.Indicator_Splits", new[] { "IndicatorId" });
            DropIndex("dbo.Objectives", new[] { "ManagerId" });
            DropIndex("dbo.Indicators", new[] { "ManagerId" });
            DropIndex("dbo.Indicators", new[] { "IndicatorTypeId" });
            DropIndex("dbo.Dashboards", new[] { "ManagerId" });
            DropTable("dbo.Dashboard_Indicators");
            DropTable("dbo.Objective_Indicators");
            DropTable("dbo.Project_Types");
            DropTable("dbo.Project_Milestones");
            DropTable("dbo.Projects");
            DropTable("dbo.Indicator_Values");
            DropTable("dbo.Indicator_Types");
            DropTable("dbo.Indicator_Split_RecordValues");
            DropTable("dbo.Indicator_Splits");
            DropTable("dbo.Objectives");
            DropTable("dbo.Users");
            DropTable("dbo.Indicators");
            DropTable("dbo.Dashboards");
        }
    }
}
