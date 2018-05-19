namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredproperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Orders", "ApplicationUserId");
            AddForeignKey("dbo.Orders", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Orders", "ApplicationUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Categories", "Name", c => c.String());
            CreateIndex("dbo.Orders", "ApplicationUserId");
            AddForeignKey("dbo.Orders", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
