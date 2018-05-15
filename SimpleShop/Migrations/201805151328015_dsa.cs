namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductUnits",
                c => new
                    {
                        ProductUnitId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductUnitId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Products", "Img", c => c.String());
            AddColumn("dbo.Products", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            CreateIndex("dbo.Products", "ApplicationUser_Id");
            AddForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductUnits", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductUnits", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            DropColumn("dbo.Products", "ApplicationUser_Id");
            DropColumn("dbo.Products", "Img");
            DropTable("dbo.ProductUnits");
        }
    }
}
