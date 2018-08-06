namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Subject", c => c.String());
            AddColumn("dbo.Events", "Description", c => c.String());
            AddColumn("dbo.Events", "Start", c => c.DateTime());
            AddColumn("dbo.Events", "EventTime", c => c.String());
            AddColumn("dbo.Events", "StreetAddress", c => c.String());
            AddColumn("dbo.Events", "City", c => c.String());
            AddColumn("dbo.Events", "State", c => c.String());
            AddColumn("dbo.Events", "Zipcode", c => c.String());
            AddColumn("dbo.Shows", "SoundCloudLink", c => c.String());
            DropColumn("dbo.Events", "EventDate");
            DropColumn("dbo.Shows", "ItunesName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shows", "ItunesName", c => c.String());
            AddColumn("dbo.Events", "EventDate", c => c.DateTime());
            DropColumn("dbo.Shows", "SoundCloudLink");
            DropColumn("dbo.Events", "Zipcode");
            DropColumn("dbo.Events", "State");
            DropColumn("dbo.Events", "City");
            DropColumn("dbo.Events", "StreetAddress");
            DropColumn("dbo.Events", "EventTime");
            DropColumn("dbo.Events", "Start");
            DropColumn("dbo.Events", "Description");
            DropColumn("dbo.Events", "Subject");
        }
    }
}
