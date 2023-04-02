namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v19 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Order", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.Product", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.Customer", "Order_Id", "dbo.Order");
            DropIndex("dbo.Product", new[] { "Order_Id" });
            DropIndex("dbo.Customer", new[] { "Order_Id" });
            DropIndex("dbo.Order", new[] { "Product_Id" });
            DropIndex("dbo.Order", new[] { "Customer_Id" });
            DropColumn("dbo.Product", "Order_Id");
            DropColumn("dbo.Customer", "Order_Id");
            DropColumn("dbo.Order", "Product_Id");
            DropColumn("dbo.Order", "Customer_Id");
            AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.Order", "CustomerId", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            AddColumn("dbo.Order", "Customer_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Order", "Product_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Customer", "Order_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Product", "Order_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Order", "Customer_Id");
            CreateIndex("dbo.Order", "Product_Id");
            CreateIndex("dbo.Customer", "Order_Id");
            CreateIndex("dbo.Product", "Order_Id");
            AddForeignKey("dbo.Customer", "Order_Id", "dbo.Order", "Id");
            AddForeignKey("dbo.Product", "Order_Id", "dbo.Order", "Id");
            AddForeignKey("dbo.Order", "Customer_Id", "dbo.Customer", "Id");
            AddForeignKey("dbo.Order", "Product_Id", "dbo.Product", "Id");
        }
    }
}
