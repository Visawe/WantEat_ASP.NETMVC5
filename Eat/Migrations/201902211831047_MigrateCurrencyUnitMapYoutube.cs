namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateCurrencyUnitMapYoutube : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Restaurants", "AverageCheckRestaurantCurrencyId", c => c.Int());
            AddColumn("dbo.Restaurants", "PathGoogleMap", c => c.String());
            AddColumn("dbo.Restaurants", "PathYoutube", c => c.String());
            AddColumn("dbo.Restaurants", "CurrencyAverageCheckRestaurant_Id", c => c.Int());
            AddColumn("dbo.Dishes", "WeightUnitId", c => c.Int());
            AddColumn("dbo.Dishes", "PriceCurrencyId", c => c.Int());
            AddColumn("dbo.Dishes", "CurrencyPrice_Id", c => c.Int());
            AddColumn("dbo.Dishes", "UnitWeight_Id", c => c.Int());
            CreateIndex("dbo.Restaurants", "CurrencyAverageCheckRestaurant_Id");
            CreateIndex("dbo.Dishes", "CurrencyPrice_Id");
            CreateIndex("dbo.Dishes", "UnitWeight_Id");
            AddForeignKey("dbo.Restaurants", "CurrencyAverageCheckRestaurant_Id", "dbo.Currencies", "Id");
            AddForeignKey("dbo.Dishes", "CurrencyPrice_Id", "dbo.Currencies", "Id");
            AddForeignKey("dbo.Dishes", "UnitWeight_Id", "dbo.Units", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dishes", "UnitWeight_Id", "dbo.Units");
            DropForeignKey("dbo.Dishes", "CurrencyPrice_Id", "dbo.Currencies");
            DropForeignKey("dbo.Restaurants", "CurrencyAverageCheckRestaurant_Id", "dbo.Currencies");
            DropIndex("dbo.Dishes", new[] { "UnitWeight_Id" });
            DropIndex("dbo.Dishes", new[] { "CurrencyPrice_Id" });
            DropIndex("dbo.Restaurants", new[] { "CurrencyAverageCheckRestaurant_Id" });
            DropColumn("dbo.Dishes", "UnitWeight_Id");
            DropColumn("dbo.Dishes", "CurrencyPrice_Id");
            DropColumn("dbo.Dishes", "PriceCurrencyId");
            DropColumn("dbo.Dishes", "WeightUnitId");
            DropColumn("dbo.Restaurants", "CurrencyAverageCheckRestaurant_Id");
            DropColumn("dbo.Restaurants", "PathYoutube");
            DropColumn("dbo.Restaurants", "PathGoogleMap");
            DropColumn("dbo.Restaurants", "AverageCheckRestaurantCurrencyId");
            DropTable("dbo.Units");
            DropTable("dbo.Currencies");
        }
    }
}
