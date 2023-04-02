namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Status", c => c.String(defaultValue:"Chờ xác nhận"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Status");
        }
    }
}
