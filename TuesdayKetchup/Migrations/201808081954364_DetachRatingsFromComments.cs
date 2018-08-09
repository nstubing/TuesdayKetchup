namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetachRatingsFromComments : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Rating", c => c.Int());
        }
    }
}
