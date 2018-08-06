namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowAddedToEpisode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Episodes", "ShowId", c => c.Int(nullable: false));
            CreateIndex("dbo.Episodes", "ShowId");
            AddForeignKey("dbo.Episodes", "ShowId", "dbo.Shows", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Episodes", "ShowId", "dbo.Shows");
            DropIndex("dbo.Episodes", new[] { "ShowId" });
            DropColumn("dbo.Episodes", "ShowId");
        }
    }
}
