namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateNullableDatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GoogleRestaurantDetails", "DateTimeLastUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GoogleRestaurantDetails", "DateTimeLastUpdate", c => c.DateTime(nullable: false));
        }
    }
}
