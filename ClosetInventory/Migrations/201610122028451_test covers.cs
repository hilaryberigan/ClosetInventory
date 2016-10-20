namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testcovers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Covers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmallFile = c.String(),
                        LargeFile = c.String(),
                        IsFavorite = c.Boolean(nullable: false),
                        DressinessRating = c.Int(),
                        WarmthRating = c.Int(),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Covers");
        }
    }
}
