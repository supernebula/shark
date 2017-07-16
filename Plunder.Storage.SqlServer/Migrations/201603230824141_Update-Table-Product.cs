namespace Plunder.Storage.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "UpdateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "UpdateTime", c => c.DateTime(nullable: false));
        }
    }
}
