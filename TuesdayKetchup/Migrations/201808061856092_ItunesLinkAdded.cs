namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItunesLinkAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shows", "ItunesLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shows", "ItunesLink");
        }
    }
}
