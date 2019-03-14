namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBStatusOrderMoreChar2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderTable", "Status", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderTable", "Status", c => c.String(nullable: false, maxLength: 1));
        }
    }
}
