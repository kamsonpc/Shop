namespace SimpleShop.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class Addpaymentiteam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Payment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Payment");
        }
    }
}
