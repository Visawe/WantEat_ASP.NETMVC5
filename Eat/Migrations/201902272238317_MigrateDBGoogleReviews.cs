namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBGoogleReviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoogleRestaurantDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Place_id = c.String(),
                        Rating = c.Double(nullable: false),
                        DateTimeLastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.GoogleReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoogleRestaurantDetailsId = c.Int(),
                        Author_name = c.String(),
                        Author_url = c.String(),
                        Language = c.String(),
                        Profile_photo_url = c.String(),
                        Rating = c.Int(),
                        Relative_time_description = c.String(),
                        Text = c.String(),
                        Time = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GoogleRestaurantDetails", t => t.GoogleRestaurantDetailsId)
                .Index(t => t.GoogleRestaurantDetailsId);
            
            AddColumn("dbo.Restaurants", "GoogleRestaurantDetailsId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoogleRestaurantDetails", "Id", "dbo.Restaurants");
            DropForeignKey("dbo.GoogleReviews", "GoogleRestaurantDetailsId", "dbo.GoogleRestaurantDetails");
            DropIndex("dbo.GoogleReviews", new[] { "GoogleRestaurantDetailsId" });
            DropIndex("dbo.GoogleRestaurantDetails", new[] { "Id" });
            DropColumn("dbo.Restaurants", "GoogleRestaurantDetailsId");
            DropTable("dbo.GoogleReviews");
            DropTable("dbo.GoogleRestaurantDetails");
        }
    }
}
