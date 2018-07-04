namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onetooneaddadres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameAndSurname = c.String(nullable: false, maxLength: 255),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 255),
                        CityCode = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "NameAndSurname");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "CityCode");
            DropColumn("dbo.AspNetUsers", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "CityCode", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.AspNetUsers", "NameAndSurname", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.UserAddresses", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserAddresses", new[] { "User_Id" });
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false));
            DropTable("dbo.UserAddresses");
        }
    }
}
