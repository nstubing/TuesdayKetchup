namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentFlags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Counter = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.Comments", t => t.CommentID, cascadeDelete: true)
                .Index(t => t.CommentID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        EpisodeId = c.Int(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Episodes", t => t.EpisodeId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EpisodeId);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Details = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventDate = c.DateTime(),
                        Details = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostFlags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Counter = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ThreadId = c.Int(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Threads", t => t.ThreadId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ThreadId);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Details = c.String(),
                        Image = c.String(),
                        ItunesName = c.String(),
                        PatreonId = c.String(),
                        TwitterAccount = c.String(),
                        NavImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TextAlerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TextId = c.Int(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Texts", t => t.TextId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TextId);
            
            CreateTable(
                "dbo.Texts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        EpisodeId = c.Int(nullable: false),
                        Message = c.String(),
                        ItunesLink = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Episodes", t => t.EpisodeId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EpisodeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextAlerts", "TextId", "dbo.Texts");
            DropForeignKey("dbo.Texts", "EpisodeId", "dbo.Episodes");
            DropForeignKey("dbo.Texts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TextAlerts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostFlags", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostFlags", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentFlags", "CommentID", "dbo.Comments");
            DropForeignKey("dbo.Comments", "EpisodeId", "dbo.Episodes");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentFlags", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Texts", new[] { "EpisodeId" });
            DropIndex("dbo.Texts", new[] { "UserId" });
            DropIndex("dbo.TextAlerts", new[] { "TextId" });
            DropIndex("dbo.TextAlerts", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "ThreadId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.PostFlags", new[] { "UserID" });
            DropIndex("dbo.PostFlags", new[] { "PostID" });
            DropIndex("dbo.Comments", new[] { "EpisodeId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.CommentFlags", new[] { "UserID" });
            DropIndex("dbo.CommentFlags", new[] { "CommentID" });
            DropTable("dbo.Texts");
            DropTable("dbo.TextAlerts");
            DropTable("dbo.Shows");
            DropTable("dbo.Threads");
            DropTable("dbo.Posts");
            DropTable("dbo.PostFlags");
            DropTable("dbo.Events");
            DropTable("dbo.Episodes");
            DropTable("dbo.Comments");
            DropTable("dbo.CommentFlags");
        }
    }
}
