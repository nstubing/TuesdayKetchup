namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinishedTextAlert : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextAlerts", "ShowId", c => c.Int(nullable: false));
            AlterColumn("dbo.TextAlerts", "EpisodeLink", c => c.String(nullable: false));
            AlterColumn("dbo.TextAlerts", "Message", c => c.String(nullable: false));
            CreateIndex("dbo.TextAlerts", "ShowId");
            AddForeignKey("dbo.TextAlerts", "ShowId", "dbo.Shows", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextAlerts", "ShowId", "dbo.Shows");
            DropIndex("dbo.TextAlerts", new[] { "ShowId" });
            AlterColumn("dbo.TextAlerts", "Message", c => c.String());
            AlterColumn("dbo.TextAlerts", "EpisodeLink", c => c.String());
            DropColumn("dbo.TextAlerts", "ShowId");
        }
    }
}
