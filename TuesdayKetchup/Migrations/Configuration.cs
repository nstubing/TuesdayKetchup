namespace TuesdayKetchup.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TuesdayKetchup.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TuesdayKetchup.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TuesdayKetchup.Models.ApplicationDbContext context)
        {
     
        }
    }
}
