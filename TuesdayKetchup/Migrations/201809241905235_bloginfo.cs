namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bloginfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Description", c => c.String());
            AddColumn("dbo.Blogs", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Image");
            DropColumn("dbo.Blogs", "Description");
        }
    }
}
