namespace GiftShop1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producto_model_change : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PurchasesProducts", newName: "ProductPurchases");
            DropPrimaryKey("dbo.ProductPurchases");
            AddPrimaryKey("dbo.ProductPurchases", new[] { "Product_productID", "Purchases_purchaseID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductPurchases");
            AddPrimaryKey("dbo.ProductPurchases", new[] { "Purchases_purchaseID", "Product_productID" });
            RenameTable(name: "dbo.ProductPurchases", newName: "PurchasesProducts");
        }
    }
}
