namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddphonenumbertoorderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "NameAndSurname", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "CityCode", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Country", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Country", c => c.String());
            AlterColumn("dbo.Orders", "CityCode", c => c.String());
            AlterColumn("dbo.Orders", "Address", c => c.String());
            AlterColumn("dbo.Orders", "NameAndSurname", c => c.String());
            DropColumn("dbo.Orders", "PhoneNumber");
        }
    }
}
