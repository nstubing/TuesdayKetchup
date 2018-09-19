namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pinnedadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Pinned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Pinned");
        }
    }
}
