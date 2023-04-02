namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Phone", c => c.String());
            AddColumn("dbo.Order", "Address", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Address");
            DropColumn("dbo.Order", "Phone");
        }
    }
}
