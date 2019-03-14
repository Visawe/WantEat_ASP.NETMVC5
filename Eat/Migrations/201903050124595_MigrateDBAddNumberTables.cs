namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBAddNumberTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "NumberTables", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "NumberTables");
        }
    }
}
