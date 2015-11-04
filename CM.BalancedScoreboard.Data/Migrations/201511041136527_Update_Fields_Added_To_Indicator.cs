namespace CM.BalancedScoreboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Fields_Added_To_Indicator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicators", "FulfillmentRate", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Indicators", "FulfillmentRate");
        }
    }
}
