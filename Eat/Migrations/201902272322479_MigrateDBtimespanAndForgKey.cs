namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBtimespanAndForgKey : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkSchedules", "TimeFrom", c => c.Time(nullable: false, precision: 0));
            AlterColumn("dbo.WorkSchedules", "TimeTo", c => c.Time(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkSchedules", "TimeTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WorkSchedules", "TimeFrom", c => c.DateTime(nullable: false));
        }
    }
}
