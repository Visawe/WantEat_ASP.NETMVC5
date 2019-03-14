namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBaddGeoLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoIPLocations",
                c => new
                    {
                        network = c.String(nullable: false, maxLength: 150),
                        geoname_id = c.Int(),
                        registered_country_geoname_id = c.Int(),
                        represented_country_geoname_id = c.String(maxLength: 100),
                        is_anonymous_proxy = c.Int(),
                        is_satellite_provider = c.Int(),
                        postal_code = c.String(maxLength: 150),
                        latitude = c.Double(),
                        longitude = c.Double(),
                        accuracy_radius = c.Int(),
                        networkFrom = c.Int(),
                        networkTo = c.Int(),
                    })
                .PrimaryKey(t => t.network)
                .ForeignKey("dbo.GeoNames", t => t.geoname_id)
                .Index(t => t.geoname_id);
            
            CreateTable(
                "dbo.GeoNames",
                c => new
                    {
                        geoname_id = c.Int(nullable: false),
                        locale_code = c.String(maxLength: 150),
                        continent_code = c.String(maxLength: 150),
                        continent_name = c.String(maxLength: 150),
                        country_iso_code = c.String(maxLength: 150),
                        country_name = c.String(maxLength: 150),
                        subdivision_1_iso_code = c.String(maxLength: 150),
                        subdivision_1_name = c.String(maxLength: 150),
                        subdivision_2_iso_code = c.String(maxLength: 150),
                        subdivision_2_name = c.String(maxLength: 150),
                        city_name = c.String(maxLength: 150),
                        metro_code = c.String(maxLength: 50),
                        time_zone = c.String(maxLength: 150),
                        is_in_european_union = c.Int(),
                    })
                .PrimaryKey(t => t.geoname_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeoIPLocations", "geoname_id", "dbo.GeoNames");
            DropIndex("dbo.GeoIPLocations", new[] { "geoname_id" });
            DropTable("dbo.GeoNames");
            DropTable("dbo.GeoIPLocations");
        }
    }
}
