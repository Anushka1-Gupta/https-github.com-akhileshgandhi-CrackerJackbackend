namespace AngularJSAuthentication.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppHomeSectionItems", "VendorId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppHomeSectionItems", "VendorId");
        }
    }
}
