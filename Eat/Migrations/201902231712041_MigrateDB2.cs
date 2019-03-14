namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Description", c => c.String(nullable: false, maxLength: 3500));
            AlterColumn("dbo.Restaurants", "Slogan", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Slogan", c => c.String());
            AlterColumn("dbo.Restaurants", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
