namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBDatetime2OrderDish : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDishesDetails", "PriceCurrencyId", c => c.Int());
            AlterColumn("dbo.OrdersDishes", "OrderTime", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.OrdersDishes", "TimeCreated", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.OrdersDishes", "TimeChanged", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.OrderTable", "OrderTimeFrom", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.OrderTable", "OrderTimeTo", c => c.DateTime(precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.OrderTable", "TimeCreated", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.OrderTable", "TimeChanged", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            CreateIndex("dbo.OrderDishesDetails", "PriceCurrencyId");
            AddForeignKey("dbo.OrderDishesDetails", "PriceCurrencyId", "dbo.Currencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDishesDetails", "PriceCurrencyId", "dbo.Currencies");
            DropIndex("dbo.OrderDishesDetails", new[] { "PriceCurrencyId" });
            AlterColumn("dbo.OrderTable", "TimeChanged", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrderTable", "TimeCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrderTable", "OrderTimeTo", c => c.DateTime());
            AlterColumn("dbo.OrderTable", "OrderTimeFrom", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrdersDishes", "TimeChanged", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrdersDishes", "TimeCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrdersDishes", "OrderTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.OrderDishesDetails", "PriceCurrencyId");
        }
    }
}
