namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ProductId", c => c.String(maxLength: 128));
            AddColumn("dbo.Order", "CustomerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Order", "ProductId");
            CreateIndex("dbo.Order", "CustomerId");
            AddForeignKey("dbo.Order", "CustomerId", "dbo.Customer", "Id");
            AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "Id");
            DropColumn("dbo.Order", "CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "CartId", c => c.String());
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropColumn("dbo.Order", "CustomerId");
            DropColumn("dbo.Order", "ProductId");
        }
    }
}
