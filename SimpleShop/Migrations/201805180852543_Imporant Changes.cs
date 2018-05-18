namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImporantChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderProductViewModels",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Payment = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProductViewModels", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderProductViewModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.OrderProductViewModels", new[] { "ApplicationUserId" });
            DropIndex("dbo.OrderProductViewModels", new[] { "ProductId" });
            DropTable("dbo.OrderProductViewModels");
        }
    }
}
