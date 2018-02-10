namespace GiftShop1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        token = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        purchaseID = c.Int(nullable: false, identity: true),
                        createdAt = c.DateTime(nullable: false),
                        boughtAt = c.DateTime(nullable: false),
                        buyerID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.purchaseID)
                .ForeignKey("dbo.AspNetUsers", t => t.buyerID)
                .Index(t => t.buyerID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        productID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.productID);
            
            CreateTable(
                "dbo.ProductPurchases",
                c => new
                    {
                        Product_productID = c.Int(nullable: false),
                        Purchases_purchaseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_productID, t.Purchases_purchaseID })
                .ForeignKey("dbo.Products", t => t.Product_productID, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.Purchases_purchaseID, cascadeDelete: true)
                .Index(t => t.Product_productID)
                .Index(t => t.Purchases_purchaseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductPurchases", "Purchases_purchaseID", "dbo.Purchases");
            DropForeignKey("dbo.ProductPurchases", "Product_productID", "dbo.Products");
            DropForeignKey("dbo.Purchases", "buyerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.ProductPurchases", new[] { "Purchases_purchaseID" });
            DropIndex("dbo.ProductPurchases", new[] { "Product_productID" });
            DropIndex("dbo.Purchases", new[] { "buyerID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.ProductPurchases");
            DropTable("dbo.Products");
            DropTable("dbo.Purchases");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
