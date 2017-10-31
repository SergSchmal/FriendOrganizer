namespace FriendOrganizer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFriendPhoneNumbers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendPhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false),
                        FrinedId = c.Int(nullable: false),
                        Friend_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friends", t => t.Friend_Id, cascadeDelete: true)
                .Index(t => t.Friend_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendPhoneNumbers", "Friend_Id", "dbo.Friends");
            DropIndex("dbo.FriendPhoneNumbers", new[] { "Friend_Id" });
            DropTable("dbo.FriendPhoneNumbers");
        }
    }
}
