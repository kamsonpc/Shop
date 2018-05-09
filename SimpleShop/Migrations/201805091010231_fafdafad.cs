namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fafdafad : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ProductViewModels");
        }
        
        public override void Down()
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
            
        }
    }
}
