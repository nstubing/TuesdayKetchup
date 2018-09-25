namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class neededd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "Title", c => c.String());
            AddColumn("dbo.BlogPosts", "Message1", c => c.String());
            AddColumn("dbo.BlogPosts", "Message2", c => c.String());
            DropColumn("dbo.BlogPosts", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogPosts", "Message", c => c.String());
            DropColumn("dbo.BlogPosts", "Message2");
            DropColumn("dbo.BlogPosts", "Message1");
            DropColumn("dbo.BlogPosts", "Title");
        }
    }
}
