namespace AngularJSAuthentication.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migtn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseCategories", "Description", c => c.String());
            AddColumn("dbo.BaseCategories", "CategoryCount", c => c.Int(nullable: false));
            DropColumn("dbo.BaseCategories", "Discription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BaseCategories", "Discription", c => c.String());
            DropColumn("dbo.BaseCategories", "CategoryCount");
            DropColumn("dbo.BaseCategories", "Description");
        }
    }
}
