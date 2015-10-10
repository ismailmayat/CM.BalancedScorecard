namespace CM.BalancedScoreboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicators", "ValueComparisonType", c => c.Int(nullable: false));
            AddColumn("dbo.Indicators", "PeriodicityType", c => c.Int(nullable: false));
            AddColumn("dbo.Indicators", "ValueObjectType", c => c.Int(nullable: false));
            DropColumn("dbo.Indicators", "TargetType");
            DropColumn("dbo.Indicators", "Periodicity");
            DropColumn("dbo.Indicators", "ValueType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Indicators", "ValueType", c => c.Int(nullable: false));
            AddColumn("dbo.Indicators", "Periodicity", c => c.Int(nullable: false));
            AddColumn("dbo.Indicators", "TargetType", c => c.Int(nullable: false));
            DropColumn("dbo.Indicators", "ValueObjectType");
            DropColumn("dbo.Indicators", "PeriodicityType");
            DropColumn("dbo.Indicators", "ValueComparisonType");
        }
    }
}
