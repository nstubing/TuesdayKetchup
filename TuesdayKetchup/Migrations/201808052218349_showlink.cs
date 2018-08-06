namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class showlink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shows", "SoundCloudLink", c => c.String());
            DropColumn("dbo.Shows", "ItunesName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shows", "ItunesName", c => c.String());
            DropColumn("dbo.Shows", "SoundCloudLink");
        }
    }
}
