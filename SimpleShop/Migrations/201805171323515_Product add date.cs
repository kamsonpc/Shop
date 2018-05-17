namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Productadddate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "AddDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "AddDate");
        }
    }
}
