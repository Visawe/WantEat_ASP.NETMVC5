namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateCurrencyUnitMapYoutube2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Restaurants", "AverageCheckRestaurantCurrencyId");
            DropColumn("dbo.Dishes", "PriceCurrencyId");
            DropColumn("dbo.Dishes", "WeightUnitId");
            RenameColumn(table: "dbo.Restaurants", name: "CurrencyAverageCheckRestaurant_Id", newName: "AverageCheckRestaurantCurrencyId");
            RenameColumn(table: "dbo.Dishes", name: "CurrencyPrice_Id", newName: "PriceCurrencyId");
            RenameColumn(table: "dbo.Dishes", name: "UnitWeight_Id", newName: "WeightUnitId");
            RenameIndex(table: "dbo.Restaurants", name: "IX_CurrencyAverageCheckRestaurant_Id", newName: "IX_AverageCheckRestaurantCurrencyId");
            RenameIndex(table: "dbo.Dishes", name: "IX_UnitWeight_Id", newName: "IX_WeightUnitId");
            RenameIndex(table: "dbo.Dishes", name: "IX_CurrencyPrice_Id", newName: "IX_PriceCurrencyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Dishes", name: "IX_PriceCurrencyId", newName: "IX_CurrencyPrice_Id");
            RenameIndex(table: "dbo.Dishes", name: "IX_WeightUnitId", newName: "IX_UnitWeight_Id");
            RenameIndex(table: "dbo.Restaurants", name: "IX_AverageCheckRestaurantCurrencyId", newName: "IX_CurrencyAverageCheckRestaurant_Id");
            RenameColumn(table: "dbo.Dishes", name: "WeightUnitId", newName: "UnitWeight_Id");
            RenameColumn(table: "dbo.Dishes", name: "PriceCurrencyId", newName: "CurrencyPrice_Id");
            RenameColumn(table: "dbo.Restaurants", name: "AverageCheckRestaurantCurrencyId", newName: "CurrencyAverageCheckRestaurant_Id");
            AddColumn("dbo.Dishes", "WeightUnitId", c => c.Int());
            AddColumn("dbo.Dishes", "PriceCurrencyId", c => c.Int());
            AddColumn("dbo.Restaurants", "AverageCheckRestaurantCurrencyId", c => c.Int());
        }
    }
}
