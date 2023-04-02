namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(storeType: "ntext"),
                        Email = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 250),
                        Password = c.String(maxLength: 250),
                        Role = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdateUserId = c.String(),
                        CreateUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        CartId = c.String(maxLength: 128),
                        ShipMoney = c.Decimal(precision: 18, scale: 2),
                        Shipdate = c.DateTime(),
                        Total = c.Decimal(precision: 18, scale: 2),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdateUserId = c.String(),
                        CreateUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cart", t => t.CartId)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CartId)
                .Index(t => t.CreateUserId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Cart", t => t.CartId)
                .Index(t => t.CartId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(storeType: "ntext"),
                        Code = c.String(),
                        Description = c.String(storeType: "ntext"),
                        Image = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        CategoryId = c.String(maxLength: 128),
                        BranchId = c.String(maxLength: 128),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdateUserId = c.String(),
                        CreateUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brand", t => t.BranchId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(storeType: "ntext"),
                        Description = c.String(storeType: "ntext"),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdateUserId = c.String(),
                        CreateUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(storeType: "ntext"),
                        Description = c.String(storeType: "ntext"),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdateUserId = c.String(),
                        CreateUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(storeType: "ntext"),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(storeType: "ntext"),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdateUserId = c.String(),
                        CreateUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Order", "CartId", "dbo.Cart");
            DropForeignKey("dbo.Cart", "CreateUserId", "dbo.Customer");
            DropForeignKey("dbo.CartDetail", "CartId", "dbo.Cart");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.CartDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "BranchId", "dbo.Brand");
            DropIndex("dbo.Product", new[] { "BranchId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.CartDetail", new[] { "ProductId" });
            DropIndex("dbo.CartDetail", new[] { "CartId" });
            DropIndex("dbo.Cart", new[] { "CreateUserId" });
            DropIndex("dbo.Order", new[] { "CreateUserId" });
            DropIndex("dbo.Order", new[] { "CartId" });
            DropTable("dbo.Customer");
            DropTable("dbo.Category");
            DropTable("dbo.Brand");
            DropTable("dbo.Product");
            DropTable("dbo.CartDetail");
            DropTable("dbo.Cart");
            DropTable("dbo.Order");
            DropTable("dbo.Account");
        }
    }
}
