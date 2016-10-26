namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class weather : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weathers", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Weathers", "Date");
        }
    }
}
