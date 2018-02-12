namespace GiftShop1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTable_ProductInCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductPurchaseCarts", "Product_productID", "dbo.Products");
            DropForeignKey("dbo.ProductPurchaseCarts", "PurchaseCart_purchaseID", "dbo.PurchaseCarts");
            DropIndex("dbo.ProductPurchaseCarts", new[] { "Product_productID" });
            DropIndex("dbo.ProductPurchaseCarts", new[] { "PurchaseCart_purchaseID" });
            CreateTable(
                "dbo.ProductInCarts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        qty = c.Int(nullable: false),
                        productID = c.Int(nullable: false),
                        buyerID = c.String(maxLength: 128),
                        PurchaseCart_purchaseID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.buyerID)
                .ForeignKey("dbo.Products", t => t.productID, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseCarts", t => t.PurchaseCart_purchaseID)
                .Index(t => t.productID)
                .Index(t => t.buyerID)
                .Index(t => t.PurchaseCart_purchaseID);
            
            DropTable("dbo.ProductPurchaseCarts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductPurchaseCarts",
                c => new
                    {
                        Product_productID = c.Int(nullable: false),
                        PurchaseCart_purchaseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_productID, t.PurchaseCart_purchaseID });
            
            DropForeignKey("dbo.ProductInCarts", "PurchaseCart_purchaseID", "dbo.PurchaseCarts");
            DropForeignKey("dbo.ProductInCarts", "productID", "dbo.Products");
            DropForeignKey("dbo.ProductInCarts", "buyerID", "dbo.AspNetUsers");
            DropIndex("dbo.ProductInCarts", new[] { "PurchaseCart_purchaseID" });
            DropIndex("dbo.ProductInCarts", new[] { "buyerID" });
            DropIndex("dbo.ProductInCarts", new[] { "productID" });
            DropTable("dbo.ProductInCarts");
            CreateIndex("dbo.ProductPurchaseCarts", "PurchaseCart_purchaseID");
            CreateIndex("dbo.ProductPurchaseCarts", "Product_productID");
            AddForeignKey("dbo.ProductPurchaseCarts", "PurchaseCart_purchaseID", "dbo.PurchaseCarts", "purchaseID", cascadeDelete: true);
            AddForeignKey("dbo.ProductPurchaseCarts", "Product_productID", "dbo.Products", "productID", cascadeDelete: true);
        }
    }
}
