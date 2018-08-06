namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTextModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TextAlerts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Texts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Texts", "EpisodeId", "dbo.Episodes");
            DropForeignKey("dbo.TextAlerts", "TextId", "dbo.Texts");
            DropIndex("dbo.TextAlerts", new[] { "UserId" });
            DropIndex("dbo.TextAlerts", new[] { "TextId" });
            DropIndex("dbo.Texts", new[] { "UserId" });
            DropIndex("dbo.Texts", new[] { "EpisodeId" });
            CreateTable(
                "dbo.TextSignups",
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
            
            AddColumn("dbo.TextAlerts", "EpisodeLink", c => c.String());
            DropColumn("dbo.TextAlerts", "UserId");
            DropColumn("dbo.TextAlerts", "TextId");
            DropTable("dbo.Texts");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TextAlerts", "TextId", c => c.Int(nullable: false));
            AddColumn("dbo.TextAlerts", "UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.TextSignups", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.TextSignups", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TextSignups", new[] { "ShowId" });
            DropIndex("dbo.TextSignups", new[] { "UserId" });
            DropColumn("dbo.TextAlerts", "EpisodeLink");
            DropTable("dbo.TextSignups");
            CreateIndex("dbo.Texts", "EpisodeId");
            CreateIndex("dbo.Texts", "UserId");
            CreateIndex("dbo.TextAlerts", "TextId");
            CreateIndex("dbo.TextAlerts", "UserId");
            AddForeignKey("dbo.TextAlerts", "TextId", "dbo.Texts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Texts", "EpisodeId", "dbo.Episodes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Texts", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TextAlerts", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
