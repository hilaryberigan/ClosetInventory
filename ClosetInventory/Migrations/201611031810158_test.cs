namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Outfits", "OccasionRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Outfits", "OccasionRating", c => c.Int(nullable: false));
        }
    }
}
