namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adsa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "ProductId", "dbo.Products");
            DropIndex("dbo.Images", new[] { "ProductId" });
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Products", "ProductId");
            CreateIndex("dbo.Products", "ProductId");
            AddForeignKey("dbo.Products", "ProductId", "dbo.Images", "ImageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductId", "dbo.Images");
            DropIndex("dbo.Products", new[] { "ProductId" });
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "ProductId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Products", "ProductId");
            CreateIndex("dbo.Images", "ProductId");
            AddForeignKey("dbo.Images", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
