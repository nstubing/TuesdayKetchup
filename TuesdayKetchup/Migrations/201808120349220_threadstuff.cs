namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class threadstuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Threads", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Threads", "UserId");
            AddForeignKey("dbo.Threads", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Threads", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Threads", new[] { "UserId" });
            DropColumn("dbo.Threads", "UserId");
        }
    }
}
