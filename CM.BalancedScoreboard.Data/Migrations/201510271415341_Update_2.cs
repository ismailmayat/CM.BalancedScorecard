namespace CM.BalancedScoreboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Indicator_Values", newName: "Indicator_Measures");
            DropForeignKey("dbo.Indicator_Splits", "IndicatorId", "dbo.Indicators");
            DropForeignKey("dbo.Indicator_Split_RecordValues", "IndicatorSplitId", "dbo.Indicator_Splits");
            DropIndex("dbo.Indicator_Splits", new[] { "IndicatorId" });
            DropIndex("dbo.Indicator_Split_RecordValues", new[] { "IndicatorSplitId" });
            AddColumn("dbo.Indicator_Measures", "RealValue", c => c.String(nullable: false));
            DropColumn("dbo.Indicators", "Splitted");
            DropColumn("dbo.Indicators", "SplitType");
            DropColumn("dbo.Indicator_Measures", "RecordValue");
            DropTable("dbo.Indicator_Splits");
            DropTable("dbo.Indicator_Split_RecordValues");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Indicator_Splits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        IndicatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Indicator_Measures", "RecordValue", c => c.String(nullable: false));
            AddColumn("dbo.Indicators", "SplitType", c => c.Int(nullable: false));
            AddColumn("dbo.Indicators", "Splitted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Indicator_Measures", "RealValue");
            CreateIndex("dbo.Indicator_Split_RecordValues", "IndicatorSplitId");
            CreateIndex("dbo.Indicator_Splits", "IndicatorId");
            AddForeignKey("dbo.Indicator_Split_RecordValues", "IndicatorSplitId", "dbo.Indicator_Splits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Indicator_Splits", "IndicatorId", "dbo.Indicators", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Indicator_Measures", newName: "Indicator_Values");
        }
    }
}
