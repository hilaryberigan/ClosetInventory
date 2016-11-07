namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useroutfitids : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CasualOutfitId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "DressyOutfitId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "BusinessCasualOutfitId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BusinessCasualOutfitId");
            DropColumn("dbo.AspNetUsers", "DressyOutfitId");
            DropColumn("dbo.AspNetUsers", "CasualOutfitId");
        }
    }
}
