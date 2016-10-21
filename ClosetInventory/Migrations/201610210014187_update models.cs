namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Covers", "HasPattern", c => c.Boolean(nullable: false));
            AddColumn("dbo.Dresses", "HasPattern", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pants", "HasPattern", c => c.Boolean(nullable: false));
            AddColumn("dbo.Shirts", "HasPattern", c => c.Boolean(nullable: false));
            AddColumn("dbo.Skirts", "HasPattern", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skirts", "HasPattern");
            DropColumn("dbo.Shirts", "HasPattern");
            DropColumn("dbo.Pants", "HasPattern");
            DropColumn("dbo.Dresses", "HasPattern");
            DropColumn("dbo.Covers", "HasPattern");
        }
    }
}
