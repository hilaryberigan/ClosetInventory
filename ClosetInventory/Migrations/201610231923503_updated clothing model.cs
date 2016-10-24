namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedclothingmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pants", "isShorts", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pants", "isShorts");
        }
    }
}
