namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastwornadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Covers", "lastWorn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Dresses", "lastWorn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pants", "lastWorn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shirts", "lastWorn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shoes", "lastWorn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Skirts", "lastWorn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skirts", "lastWorn");
            DropColumn("dbo.Shoes", "lastWorn");
            DropColumn("dbo.Shirts", "lastWorn");
            DropColumn("dbo.Pants", "lastWorn");
            DropColumn("dbo.Dresses", "lastWorn");
            DropColumn("dbo.Covers", "lastWorn");
        }
    }
}
