namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.Carts", new[] { "Customer_Id" });
            DropColumn("dbo.Carts", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Customer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Carts", "Customer_Id");
            AddForeignKey("dbo.Carts", "Customer_Id", "dbo.Customer", "Id");
        }
    }
}
