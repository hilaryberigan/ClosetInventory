namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class occasion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Outfits", "OccasionRating", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Outfits", "OccasionRating");
        }
    }
}
