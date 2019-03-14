namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBerrorTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkSchedules", "TimeFrom", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WorkSchedules", "TimeTo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkSchedules", "TimeTo", c => c.DateTime());
            AlterColumn("dbo.WorkSchedules", "TimeFrom", c => c.DateTime());
        }
    }
}
