namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BannerDone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "Score", c => c.Int(nullable: false));
            DropColumn("dbo.Comments", "Rating");
            DropColumn("dbo.Ratings", "Star");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "Star", c => c.Double(nullable: false));
            AddColumn("dbo.Comments", "Rating", c => c.Int());
            DropColumn("dbo.Ratings", "Score");
        }
    }
}
