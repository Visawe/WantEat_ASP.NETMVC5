namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateAddMinutes3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderTable", "BookingMinutes", c => c.Int());
            AddColumn("dbo.WorkSchedules", "WorkMinutes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkSchedules", "WorkMinutes");
            DropColumn("dbo.OrderTable", "BookingMinutes");
        }
    }
}
