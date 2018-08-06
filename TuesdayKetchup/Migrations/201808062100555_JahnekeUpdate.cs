namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JahnekeUpdate : DbMigration
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        SoundCloudLink = c.String(),
                        ShowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.ShowId, cascadeDelete: true)
                .Index(t => t.ShowId);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Details = c.String(),
                        Image = c.String(),
                        SoundCloudLink = c.String(),
                        PatreonId = c.String(),
                        TwitterAccount = c.String(),
                        NavImage = c.String(),
                        ItunesLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        Start = c.DateTime(),
                        EventTime = c.String(),
                        Details = c.String(),
                        Image = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TextAlerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EpisodeLink = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        ShowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.ShowId, cascadeDelete: true)
                .Index(t => t.ShowId);
            
            CreateTable(
                "dbo.Texts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ShowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Shows", t => t.ShowId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ShowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Texts", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.Texts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TextAlerts", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PostFlags", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostFlags", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentFlags", "CommentID", "dbo.Comments");
            DropForeignKey("dbo.Comments", "EpisodeId", "dbo.Episodes");
            DropForeignKey("dbo.Episodes", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentFlags", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Texts", new[] { "ShowId" });
            DropIndex("dbo.Texts", new[] { "UserId" });
            DropIndex("dbo.TextAlerts", new[] { "ShowId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Posts", new[] { "ThreadId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.PostFlags", new[] { "UserID" });
            DropIndex("dbo.PostFlags", new[] { "PostID" });
            DropIndex("dbo.Episodes", new[] { "ShowId" });
            DropIndex("dbo.Comments", new[] { "EpisodeId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CommentFlags", new[] { "UserID" });
            DropIndex("dbo.CommentFlags", new[] { "CommentID" });
            DropTable("dbo.Texts");
            DropTable("dbo.TextAlerts");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Threads");
            DropTable("dbo.Posts");
            DropTable("dbo.PostFlags");
            DropTable("dbo.Events");
            DropTable("dbo.Shows");
            DropTable("dbo.Episodes");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CommentFlags");
        }
    }
}
