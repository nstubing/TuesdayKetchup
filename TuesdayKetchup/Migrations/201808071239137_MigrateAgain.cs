namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Episodes", "SoundCloudLink", c => c.String());
            AddColumn("dbo.Episodes", "ShowId", c => c.Int(nullable: false));
            AddColumn("dbo.Shows", "ItunesLink", c => c.String());
            CreateIndex("dbo.Episodes", "ShowId");
            AddForeignKey("dbo.Episodes", "ShowId", "dbo.Shows", "Id", cascadeDelete: true);
            DropColumn("dbo.Episodes", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Episodes", "Image", c => c.String());
            DropForeignKey("dbo.Episodes", "ShowId", "dbo.Shows");
            DropIndex("dbo.Episodes", new[] { "ShowId" });
            DropColumn("dbo.Shows", "ItunesLink");
            DropColumn("dbo.Episodes", "ShowId");
            DropColumn("dbo.Episodes", "SoundCloudLink");
        }
    }
}
