namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.CartDetail", "CartId", "dbo.Cart");
            DropForeignKey("dbo.Cart", "CreateUserId", "dbo.Customer");
            DropForeignKey("dbo.Order", "CartId", "dbo.Cart");
            DropIndex("dbo.Order", new[] { "CartId" });
            DropIndex("dbo.Cart", new[] { "CreateUserId" });
            DropIndex("dbo.CartDetail", new[] { "CartId" });
            DropIndex("dbo.CartDetail", new[] { "ProductId" });
            AlterColumn("dbo.Order", "CartId", c => c.String());
            DropTable("dbo.Cart");
            DropTable("dbo.CartDetail");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartDetail",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        Total = c.Decimal(precision: 18, scale: 2),
                        CartId = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdateUserId = c.String(),
                        CreateUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Total = c.Decimal(precision: 18, scale: 2),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdateUserId = c.String(),
                        CreateUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Order", "CartId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CartDetail", "ProductId");
            CreateIndex("dbo.CartDetail", "CartId");
            CreateIndex("dbo.Cart", "CreateUserId");
            CreateIndex("dbo.Order", "CartId");
            AddForeignKey("dbo.Order", "CartId", "dbo.Cart", "Id");
            AddForeignKey("dbo.Cart", "CreateUserId", "dbo.Customer", "Id");
            AddForeignKey("dbo.CartDetail", "CartId", "dbo.Cart", "Id");
            AddForeignKey("dbo.CartDetail", "ProductId", "dbo.Product", "Id");
        }
    }
}
