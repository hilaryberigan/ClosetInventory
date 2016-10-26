namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class temp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Weathers", "Temperature", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Weathers", "Temperature", c => c.String());
        }
    }
}
