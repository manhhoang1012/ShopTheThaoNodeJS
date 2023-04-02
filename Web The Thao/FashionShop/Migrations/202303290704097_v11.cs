namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "SessionId", "dbo.Customer");
            DropIndex("dbo.Carts", new[] { "SessionId" });
            AddColumn("dbo.Carts", "Customer_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Carts", "SessionId", c => c.String());
            CreateIndex("dbo.Carts", "Customer_Id");
            AddForeignKey("dbo.Carts", "Customer_Id", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.Carts", new[] { "Customer_Id" });
            AlterColumn("dbo.Carts", "SessionId", c => c.String(maxLength: 128));
            DropColumn("dbo.Carts", "Customer_Id");
            CreateIndex("dbo.Carts", "SessionId");
            AddForeignKey("dbo.Carts", "SessionId", "dbo.Customer", "Id");
        }
    }
}
