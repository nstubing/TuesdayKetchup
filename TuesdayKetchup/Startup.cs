using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.Linq;
using TuesdayKetchup.Models;

[assembly: OwinStartupAttribute(typeof(TuesdayKetchup.Startup))]
namespace TuesdayKetchup
{
    public partial class Startup
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
            SeedPods();

        }
        private void SeedPods()
        {
            
            Show Ttk = new Show();
            Ttk.Title = "The Tuesday Ketchup";
            Ttk.Details = "6 good friends & guests getting together every week to Ketch-up, talk about life, current events, and Kurt Russell movies. Come Ketch-up with us.";
            Ttk.Image = "~/Content/TuesdayKetchup.jpg";
            Ttk.TwitterAccount = "@Tuesday_Ketchup";
            Ttk.SoundCloudLink = "https://soundcloud.com/user-226156957";
            Ttk.NavImage = "~/Content/ketchupred.png";
            context.shows.Add(Ttk);
            Show Nick = new Show();
            Nick.Title = "Nick @ Night";
            Nick.Details = "Just laugh a little bit.";
            Nick.Image = "~/Content/NickAtNight.PNG";
            Nick.SoundCloudLink = "https://soundcloud.com/nick-argall-493249104";
            Nick.NavImage = "~/Content/greenDrip.png";
            context.shows.Add(Nick);
            context.SaveChanges();
        }

        private void CreateRolesandUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "admin@trashcollector.com";
                string userPWD = "poiuyt";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("Fan"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Fan";
                roleManager.Create(role);
            }


        }


    }
}
