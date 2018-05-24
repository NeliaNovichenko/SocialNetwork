namespace FinalProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changechatmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            DropPrimaryKey("dbo.Chats");
            AlterColumn("dbo.Chats", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Messages", "Chat_Id", c => c.Int());
            AddPrimaryKey("dbo.Chats", "Id");
            CreateIndex("dbo.Messages", "Chat_Id");
            AddForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            DropPrimaryKey("dbo.Chats");
            AlterColumn("dbo.Messages", "Chat_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Chats", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Chats", "Id");
            CreateIndex("dbo.Messages", "Chat_Id");
            AddForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats", "Id");
        }
    }
}
