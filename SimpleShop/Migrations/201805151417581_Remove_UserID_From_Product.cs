namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_UserID_From_Product : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Products", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Products", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "ApplicationUser_Id");
            AddForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
