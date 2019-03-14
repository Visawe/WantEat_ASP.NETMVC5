namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBAddGroupTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "GroupId", c => c.Int());
            CreateIndex("dbo.Tables", "GroupId");
            AddForeignKey("dbo.Tables", "GroupId", "dbo.Tables", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tables", "GroupId", "dbo.Tables");
            DropIndex("dbo.Tables", new[] { "GroupId" });
            DropColumn("dbo.Tables", "GroupId");
        }
    }
}
