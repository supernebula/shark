namespace Plunder.Plugin.Storage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableUrl2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Url",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SiteId = c.String(maxLength: 300),
                        Channel = c.String(),
                        Hash = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 300),
                        HttpMethod = c.String(nullable: false, maxLength: 10),
                        UrlType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        AlreadyRetryCount = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Url");
        }
    }
}
