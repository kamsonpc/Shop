namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrdersModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductUnits", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductUnits", new[] { "ProductId" });
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ApplicationUserId);
            
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            DropTable("dbo.ProductUnits");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductUnits",
                c => new
                    {
                        ProductUnitId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductUnitId);
            
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "ApplicationUserId" });
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropColumn("dbo.Products", "Quantity");
            DropTable("dbo.Orders");
            CreateIndex("dbo.ProductUnits", "ProductId");
            AddForeignKey("dbo.ProductUnits", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
