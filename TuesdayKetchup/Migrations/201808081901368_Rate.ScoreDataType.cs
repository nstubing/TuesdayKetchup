namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RateScoreDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "Score", c => c.Double(nullable: false));
        }
    }
}
