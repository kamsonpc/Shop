namespace SimpleShop.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class AddDataAdnotatiotion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "NameAndSurname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "NameAndSurname", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Price", c => c.Single(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.Single(nullable: false));
        }
    }
}
