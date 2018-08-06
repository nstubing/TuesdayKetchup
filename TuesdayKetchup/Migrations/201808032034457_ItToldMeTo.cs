namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItToldMeTo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TextSignups", newName: "Texts");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Texts", newName: "TextSignups");
        }
    }
}
