namespace Plunder.Storage.SqlServer.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        Price = c.Double(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "商品价格",
                                    new AnnotationValues(oldValue: null, newValue: "value")
                                },
                            }),
                        PicUri = c.String(maxLength: 200),
                        Uri = c.String(nullable: false, maxLength: 200),
                        CommentCount = c.Int(nullable: false),
                        SiteName = c.String(nullable: false, maxLength: 100),
                        SiteDomain = c.String(nullable: false, maxLength: 100),
                        ElapsedSecond = c.Double(nullable: false),
                        Downloader = c.String(nullable: false, maxLength: 100),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Product",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "Price",
                        new Dictionary<string, object>
                        {
                            { "商品价格", "value" },
                        }
                    },
                });
        }
    }
}
