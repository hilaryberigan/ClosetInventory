namespace ClosetInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isLong = c.Boolean(nullable: false),
                        SmallFile = c.String(),
                        LargeFile = c.String(),
                        IsFavorite = c.Boolean(nullable: false),
                        DressinessRating = c.Int(),
                        WarmthRating = c.Int(),
                        Color = c.String(),
                        ColorType = c.String(),
                        IsTightFit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Outfits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShirtId = c.Int(),
                        ShoeId = c.Int(),
                        PantsId = c.Int(),
                        DressId = c.Int(),
                        SkirtId = c.Int(),
                        CoverId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        WasWorn = c.Boolean(nullable: false),
                        isLiked = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Covers", t => t.CoverId)
                .ForeignKey("dbo.Dresses", t => t.DressId)
                .ForeignKey("dbo.Pants", t => t.PantsId)
                .ForeignKey("dbo.Shirts", t => t.ShirtId)
                .ForeignKey("dbo.Shoes", t => t.ShoeId)
                .ForeignKey("dbo.Skirts", t => t.SkirtId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ShirtId)
                .Index(t => t.ShoeId)
                .Index(t => t.PantsId)
                .Index(t => t.DressId)
                .Index(t => t.SkirtId)
                .Index(t => t.CoverId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Pants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isCapri = c.Boolean(nullable: false),
                        IsHighWaist = c.Boolean(nullable: false),
                        IsSkinny = c.Boolean(nullable: false),
                        SmallFile = c.String(),
                        LargeFile = c.String(),
                        IsFavorite = c.Boolean(nullable: false),
                        DressinessRating = c.Int(),
                        WarmthRating = c.Int(),
                        Color = c.String(),
                        ColorType = c.String(),
                        IsTightFit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shirts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SleeveLength = c.String(),
                        IsCropped = c.Boolean(nullable: false),
                        SmallFile = c.String(),
                        LargeFile = c.String(),
                        IsFavorite = c.Boolean(nullable: false),
                        DressinessRating = c.Int(),
                        WarmthRating = c.Int(),
                        Color = c.String(),
                        ColorType = c.String(),
                        IsTightFit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmallFile = c.String(),
                        LargeFile = c.String(),
                        IsFavorite = c.Boolean(nullable: false),
                        DressinessRating = c.Int(),
                        WarmthRating = c.Int(),
                        Color = c.String(),
                        ColorType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skirts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isLong = c.Boolean(nullable: false),
                        IsHighWaist = c.Boolean(nullable: false),
                        SmallFile = c.String(),
                        LargeFile = c.String(),
                        IsFavorite = c.Boolean(nullable: false),
                        DressinessRating = c.Int(),
                        WarmthRating = c.Int(),
                        Color = c.String(),
                        ColorType = c.String(),
                        IsTightFit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Covers", "Type", c => c.String());
            AddColumn("dbo.Covers", "ColorType", c => c.String());
            AddColumn("dbo.Covers", "IsTightFit", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Outfits", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Outfits", "SkirtId", "dbo.Skirts");
            DropForeignKey("dbo.Outfits", "ShoeId", "dbo.Shoes");
            DropForeignKey("dbo.Outfits", "ShirtId", "dbo.Shirts");
            DropForeignKey("dbo.Outfits", "PantsId", "dbo.Pants");
            DropForeignKey("dbo.Outfits", "DressId", "dbo.Dresses");
            DropForeignKey("dbo.Outfits", "CoverId", "dbo.Covers");
            DropIndex("dbo.Outfits", new[] { "UserId" });
            DropIndex("dbo.Outfits", new[] { "CoverId" });
            DropIndex("dbo.Outfits", new[] { "SkirtId" });
            DropIndex("dbo.Outfits", new[] { "DressId" });
            DropIndex("dbo.Outfits", new[] { "PantsId" });
            DropIndex("dbo.Outfits", new[] { "ShoeId" });
            DropIndex("dbo.Outfits", new[] { "ShirtId" });
            DropColumn("dbo.Covers", "IsTightFit");
            DropColumn("dbo.Covers", "ColorType");
            DropColumn("dbo.Covers", "Type");
            DropTable("dbo.Skirts");
            DropTable("dbo.Shoes");
            DropTable("dbo.Shirts");
            DropTable("dbo.Pants");
            DropTable("dbo.Outfits");
            DropTable("dbo.Dresses");
        }
    }
}
