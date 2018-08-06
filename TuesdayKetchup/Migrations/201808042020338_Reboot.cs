namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reboot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Subject", c => c.String());
            AddColumn("dbo.Events", "Description", c => c.String());
            AddColumn("dbo.Events", "Start", c => c.DateTime());
            DropColumn("dbo.Events", "EventDate");
            DropColumn("dbo.Events", "Start_Date");
            DropColumn("dbo.Events", "End_Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "End_Date", c => c.String());
            AddColumn("dbo.Events", "Start_Date", c => c.String());
            AddColumn("dbo.Events", "EventDate", c => c.DateTime());
            DropColumn("dbo.Events", "Start");
            DropColumn("dbo.Events", "Description");
            DropColumn("dbo.Events", "Subject");
        }
    }
}
