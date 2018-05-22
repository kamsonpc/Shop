namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addshippingpropertytoordermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "NameAndSurname", c => c.String());
            AddColumn("dbo.Orders", "Address", c => c.String());
            AddColumn("dbo.Orders", "CityCode", c => c.String());
            AddColumn("dbo.Orders", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Country");
            DropColumn("dbo.Orders", "CityCode");
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "NameAndSurname");
        }
    }
}
