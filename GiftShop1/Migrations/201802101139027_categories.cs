namespace GiftShop1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        categoryID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.categoryID);
            
            AddColumn("dbo.Products", "categoryID", c => c.Int());
            CreateIndex("dbo.Products", "categoryID");
            AddForeignKey("dbo.Products", "categoryID", "dbo.Categories", "categoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "categoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "categoryID" });
            DropColumn("dbo.Products", "categoryID");
            DropTable("dbo.Categories");
        }
    }
}
