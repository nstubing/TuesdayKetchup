namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thesedeets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Threads", "ThreadDetails", c => c.String());
            //DropColumn("dbo.Threads", "Details");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Threads", "Details", c => c.String());
            DropColumn("dbo.Threads", "ThreadDetails");
        }
    }
}
