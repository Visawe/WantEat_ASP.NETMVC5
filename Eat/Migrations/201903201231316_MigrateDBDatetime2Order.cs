namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBDatetime2Order : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "TimeCreated", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.Orders", "TimeChanged", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TimeChanged", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "TimeCreated", c => c.DateTime(nullable: false));
        }
    }
}
