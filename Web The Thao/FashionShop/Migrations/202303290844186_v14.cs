namespace FashionShop.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "Status", c => c.String(
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SqlDefaultValue",
                        new AnnotationValues(oldValue: null, newValue: "Chờ xác nhận")
                    },
                }));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Status", c => c.String(
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SqlDefaultValue",
                        new AnnotationValues(oldValue: "Chờ xác nhận", newValue: null)
                    },
                }));
        }
    }
}
