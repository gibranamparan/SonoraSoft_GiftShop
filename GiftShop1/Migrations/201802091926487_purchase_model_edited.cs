namespace GiftShop1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchase_model_edited : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductPurchases", newName: "PurchasesProducts");
            DropPrimaryKey("dbo.PurchasesProducts");
            AddPrimaryKey("dbo.PurchasesProducts", new[] { "Purchases_purchaseID", "Product_productID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PurchasesProducts");
            AddPrimaryKey("dbo.PurchasesProducts", new[] { "Product_productID", "Purchases_purchaseID" });
            RenameTable(name: "dbo.PurchasesProducts", newName: "ProductPurchases");
        }
    }
}
