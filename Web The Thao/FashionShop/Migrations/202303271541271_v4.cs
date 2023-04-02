namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "CreateUserId", "dbo.Account");
            DropIndex("dbo.Order", new[] { "CreateUserId" });
            AlterColumn("dbo.Order", "CreateUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "CreateUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Order", "CreateUserId");
            AddForeignKey("dbo.Order", "CreateUserId", "dbo.Account", "Id");
        }
    }
}
