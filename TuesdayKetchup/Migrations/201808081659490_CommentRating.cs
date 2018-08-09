namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentRating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "EpisodeId", "dbo.Episodes");
            DropIndex("dbo.Ratings", new[] { "EpisodeId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropTable("dbo.Ratings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EpisodeId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Star = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Ratings", "UserId");
            CreateIndex("dbo.Ratings", "EpisodeId");
            AddForeignKey("dbo.Ratings", "EpisodeId", "dbo.Episodes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
