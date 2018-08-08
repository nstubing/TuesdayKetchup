namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Message = c.String(),
                        RecipientEmail = c.String(),
                        FanEmail = c.String(),
                        SenderEmail = c.String(),
                        SenderPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Emails");
        }
    }
}
