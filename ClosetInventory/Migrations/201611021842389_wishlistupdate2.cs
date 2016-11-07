namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wishlistupdate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishListItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Title = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WishListItems");
        }
    }
}
