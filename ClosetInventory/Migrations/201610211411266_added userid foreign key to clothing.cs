namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduseridforeignkeytoclothing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Covers", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Dresses", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Pants", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Shirts", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Skirts", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Covers", "UserId");
            CreateIndex("dbo.Dresses", "UserId");
            CreateIndex("dbo.Pants", "UserId");
            CreateIndex("dbo.Shirts", "UserId");
            CreateIndex("dbo.Skirts", "UserId");
            AddForeignKey("dbo.Covers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Dresses", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Pants", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Shirts", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Skirts", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skirts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Shirts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pants", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dresses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Covers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Skirts", new[] { "UserId" });
            DropIndex("dbo.Shirts", new[] { "UserId" });
            DropIndex("dbo.Pants", new[] { "UserId" });
            DropIndex("dbo.Dresses", new[] { "UserId" });
            DropIndex("dbo.Covers", new[] { "UserId" });
            DropColumn("dbo.Skirts", "UserId");
            DropColumn("dbo.Shirts", "UserId");
            DropColumn("dbo.Pants", "UserId");
            DropColumn("dbo.Dresses", "UserId");
            DropColumn("dbo.Covers", "UserId");
        }
    }
}
