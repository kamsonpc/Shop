namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class affdsgs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductViewModels",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Price = c.Single(nullable: false),
                        Description = c.String(nullable: false, maxLength: 255),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            DropTable("dbo.ProductViewModels");
        }
    }
}
