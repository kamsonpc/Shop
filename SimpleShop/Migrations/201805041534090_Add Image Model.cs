namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImgPath = c.String(),
                    })
                .PrimaryKey(t => t.ImageId);
            
            DropColumn("dbo.Products", "ImgUrl");
            DropTable("dbo.ProductViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductViewModels",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Description = c.String(),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            AddColumn("dbo.Products", "ImgUrl", c => c.String());
            DropTable("dbo.Images");
        }
    }
}
