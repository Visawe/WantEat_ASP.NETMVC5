namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBStatusOrderMoreChar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Status", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Status", c => c.String(nullable: false, maxLength: 1));
        }
    }
}
