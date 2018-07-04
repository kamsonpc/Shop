namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixError : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserAddresses", name: "User_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.UserAddresses", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            AddColumn("dbo.UserAddresses", "ApplicationUserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAddresses", "ApplicationUserId");
            RenameIndex(table: "dbo.UserAddresses", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.UserAddresses", name: "ApplicationUser_Id", newName: "User_Id");
        }
    }
}
