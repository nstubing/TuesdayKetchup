namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomeInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Announcement = c.String(),
                        SliderPic1 = c.String(),
                        SliderPic2 = c.String(),
                        SliderPic3 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Comments", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Rating");
            DropTable("dbo.HomeInfoes");
        }
    }
}
