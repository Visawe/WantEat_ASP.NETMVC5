namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBaddOrderTableToOrderDish : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrdersDishes", "TableId", "dbo.Tables");
            DropIndex("dbo.OrdersDishes", new[] { "TableId" });
            AddColumn("dbo.OrdersDishes", "OrderTableId", c => c.Int());
            CreateIndex("dbo.OrdersDishes", "OrderTableId");
            AddForeignKey("dbo.OrdersDishes", "OrderTableId", "dbo.OrderTable", "Id");
            DropColumn("dbo.OrdersDishes", "TableId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrdersDishes", "TableId", c => c.Int());
            DropForeignKey("dbo.OrdersDishes", "OrderTableId", "dbo.OrderTable");
            DropIndex("dbo.OrdersDishes", new[] { "OrderTableId" });
            DropColumn("dbo.OrdersDishes", "OrderTableId");
            CreateIndex("dbo.OrdersDishes", "TableId");
            AddForeignKey("dbo.OrdersDishes", "TableId", "dbo.Tables", "Id");
        }
    }
}
