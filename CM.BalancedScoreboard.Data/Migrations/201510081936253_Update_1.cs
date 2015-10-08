namespace CM.BalancedScoreboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicators", "ValueType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Indicators", "ValueType");
        }
    }
}
