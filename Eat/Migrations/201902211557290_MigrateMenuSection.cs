namespace Eat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateMenuSection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Section = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Dishes", "MenuSectionId", c => c.Int());
            CreateIndex("dbo.Dishes", "MenuSectionId");
            AddForeignKey("dbo.Dishes", "MenuSectionId", "dbo.MenuSections", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dishes", "MenuSectionId", "dbo.MenuSections");
            DropIndex("dbo.Dishes", new[] { "MenuSectionId" });
            DropColumn("dbo.Dishes", "MenuSectionId");
            DropTable("dbo.MenuSections");
        }
    }
}
