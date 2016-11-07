namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useronweather : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weathers", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Weathers", "UserId");
            AddForeignKey("dbo.Weathers", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Weathers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Weathers", new[] { "UserId" });
            DropColumn("dbo.Weathers", "UserId");
        }
    }
}
