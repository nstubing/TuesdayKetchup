namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SoundcloudLinkInEp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Episodes", "SoundCloudLink", c => c.String());
            DropColumn("dbo.Episodes", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Episodes", "Image", c => c.String());
            DropColumn("dbo.Episodes", "SoundCloudLink");
        }
    }
}
