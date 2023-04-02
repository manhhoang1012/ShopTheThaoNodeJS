namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Quantity", c => c.Int());
            DropColumn("dbo.Order", "ShipMoney");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "ShipMoney", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Order", "Quantity");
        }
    }
}
