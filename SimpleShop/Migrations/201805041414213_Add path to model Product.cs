namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddpathtomodelProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImgUrl", c => c.String());
            AddColumn("dbo.ProductViewModels", "ImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductViewModels", "ImgUrl");
            DropColumn("dbo.Products", "ImgUrl");
        }
    }
}
