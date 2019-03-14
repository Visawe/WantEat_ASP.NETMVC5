namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateSlogan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "Slogan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "Slogan");
        }
    }
}
