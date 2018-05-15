namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicationUserFKForPostMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Posts", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameColumn(table: "dbo.Posts", name: "ApplicationUser_Id", newName: "ApplicationUserId");
        }
    }
}
