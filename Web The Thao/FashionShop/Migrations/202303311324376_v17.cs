namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Name");
        }
    }
}
