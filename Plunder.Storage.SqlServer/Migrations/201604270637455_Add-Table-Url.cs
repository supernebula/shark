namespace Plunder.Storage.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableUrl : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Product", name: "Name", newName: "Title");
            AlterColumn("dbo.Product", "PicUri", c => c.String(maxLength: 300));
            AlterColumn("dbo.Product", "Uri", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Uri", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Product", "PicUri", c => c.String(maxLength: 200));
            RenameColumn(table: "dbo.Product", name: "Title", newName: "Name");
        }
    }
}
