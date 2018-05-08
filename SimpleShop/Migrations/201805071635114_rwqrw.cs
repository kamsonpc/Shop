namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rwqrw : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductId", "dbo.Images");
            DropIndex("dbo.Products", new[] { "ProductId" });
            DropPrimaryKey("dbo.Products");
            AddColumn("dbo.Products", "Img", c => c.String());
            AlterColumn("dbo.Products", "ProductId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Products", "ProductId");
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImgPath = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Img");
            AddPrimaryKey("dbo.Products", "ProductId");
            CreateIndex("dbo.Products", "ProductId");
            AddForeignKey("dbo.Products", "ProductId", "dbo.Images", "ImageId");
        }
    }
}
