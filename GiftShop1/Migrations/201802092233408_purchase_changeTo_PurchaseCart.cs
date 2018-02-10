namespace GiftShop1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchase_changeTo_PurchaseCart : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Purchases", newName: "PurchaseCarts");
            RenameTable(name: "dbo.ProductPurchases", newName: "ProductPurchaseCarts");
            RenameColumn(table: "dbo.ProductPurchaseCarts", name: "Purchases_purchaseID", newName: "PurchaseCart_purchaseID");
            RenameIndex(table: "dbo.ProductPurchaseCarts", name: "IX_Purchases_purchaseID", newName: "IX_PurchaseCart_purchaseID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductPurchaseCarts", name: "IX_PurchaseCart_purchaseID", newName: "IX_Purchases_purchaseID");
            RenameColumn(table: "dbo.ProductPurchaseCarts", name: "PurchaseCart_purchaseID", newName: "Purchases_purchaseID");
            RenameTable(name: "dbo.ProductPurchaseCarts", newName: "ProductPurchases");
            RenameTable(name: "dbo.PurchaseCarts", newName: "Purchases");
        }
    }
}
