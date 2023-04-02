namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "UpdateUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Order", "UpdateUserId");
            AddForeignKey("dbo.Order", "UpdateUserId", "dbo.Account", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "UpdateUserId", "dbo.Account");
            DropIndex("dbo.Order", new[] { "UpdateUserId" });
            AlterColumn("dbo.Order", "UpdateUserId", c => c.String());
        }
    }
}
