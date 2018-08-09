namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thismiggggg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EpisodeId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Episodes", t => t.EpisodeId, cascadeDelete: true)
                .Index(t => t.EpisodeId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Comments", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Rating", c => c.Int());
            DropForeignKey("dbo.Ratings", "EpisodeId", "dbo.Episodes");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "EpisodeId" });
            DropTable("dbo.Ratings");
        }
    }
}
