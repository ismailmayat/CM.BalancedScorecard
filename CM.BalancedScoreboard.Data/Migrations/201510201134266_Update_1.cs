namespace CM.BalancedScoreboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicators", "ComparisonValueType", c => c.Int(nullable: false));
            AddColumn("dbo.Indicators", "ObjectValueType", c => c.Int(nullable: false));
            DropColumn("dbo.Indicators", "ValueComparisonType");
            DropColumn("dbo.Indicators", "ValueObjectType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Indicators", "ValueObjectType", c => c.Int(nullable: false));
            AddColumn("dbo.Indicators", "ValueComparisonType", c => c.Int(nullable: false));
            DropColumn("dbo.Indicators", "ObjectValueType");
            DropColumn("dbo.Indicators", "ComparisonValueType");
        }
    }
}
