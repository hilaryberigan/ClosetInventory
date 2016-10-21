namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedforeignkeyforuseridtoshoe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shoes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Shoes", "UserId");
            AddForeignKey("dbo.Shoes", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Shoes", new[] { "UserId" });
            DropColumn("dbo.Shoes", "UserId");
        }
    }
}
