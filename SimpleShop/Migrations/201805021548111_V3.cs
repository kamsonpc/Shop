namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductViewModels",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductViewModels");
        }
    }
}
