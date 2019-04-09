namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBaddTableToOrderDish : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdersDishes", "TableId", c => c.Int());
            CreateIndex("dbo.OrdersDishes", "TableId");
            AddForeignKey("dbo.OrdersDishes", "TableId", "dbo.Tables", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdersDishes", "TableId", "dbo.Tables");
            DropIndex("dbo.OrdersDishes", new[] { "TableId" });
            DropColumn("dbo.OrdersDishes", "TableId");
        }
    }
}
