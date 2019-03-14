namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateAddMinutes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderTable", "OrderTimeFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderTable", "OrderTimeTo", c => c.DateTime());
            DropColumn("dbo.OrderTable", "OrderTime");
            DropColumn("dbo.OrderTable", "NumberHours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderTable", "NumberHours", c => c.DateTime());
            AddColumn("dbo.OrderTable", "OrderTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.OrderTable", "OrderTimeTo");
            DropColumn("dbo.OrderTable", "OrderTimeFrom");
        }
    }
}
