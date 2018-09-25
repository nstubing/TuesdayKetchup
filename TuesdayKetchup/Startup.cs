using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
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
            SeedVids();
            SeedBlogs();
        }
        private void SeedBlogs()
        {
            var blogs = context.Blogs.Where(b => b.Title == "Nick @ Night in Words").FirstOrDefault();
            if (blogs == null)
            {
                Blog nickBlog = new Blog();
                nickBlog.Title = "Nick @ Night in Words";
                nickBlog.Image = "/Content/NickAtNight.PNG";
                nickBlog.Description = "I am nick and every day I Write some stuff about myself or whatever I want.";
                context.Blogs.Add(nickBlog);
                context.SaveChanges();
            }
        }
        private void SeedVids()
        {
            var vids = context.videos.Select(v => v).ToList();
            if(vids.Count()==0)
            {
                Video firstVideo = new Video();
                firstVideo.Pinned = true;
                firstVideo.Title = "Hot Wing Promo";
                firstVideo.YoutubeLink = "https://www.youtube.com/embed/W00vEaxH23A?rel=0";
                Video sVideo = new Video();
                sVideo.Pinned = false;
                sVideo.Title = "Tuesday Ketchup Promo";
                sVideo.YoutubeLink = "https://www.youtube.com/embed/UbKXfZhQiCU?rel=0";
                context.videos.Add(firstVideo);
                context.videos.Add(sVideo);
                context.SaveChanges();
            }
        }
        private void SeedPods()
        {
            var show = context.shows.Where(s => s.Title == "The 60 Yard Line").FirstOrDefault();
            if (show == null)
            {
                Show yardline = new Show();
                yardline.Title = "The 60 Yard Line";
                yardline.Details = "Gavin talks about sports and many other things";
                yardline.Image = "/Content/60yardline.jpg";
                yardline.TwitterAccount = "@smithgavin50";
                context.shows.Add(yardline);
                context.SaveChanges();
            }

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
                string userPWD = "Ketchup123!";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
                Show Ttk = new Show();
                Ttk.Title = "The Tuesday Ketchup";
                Ttk.Details = "6 good friends & guests getting together every week to Ketch-up, talk about life, current events, and Kurt Russell movies. Come Ketch-up with us.";
                Ttk.Image = "/Content/TuesdayKetchup.jpg";
                Ttk.TwitterAccount = "@Tuesday_Ketchup";
                Ttk.SoundCloudLink = "https://soundcloud.com/user-226156957";
                Ttk.NavImage = "~/Content/ketchupred.png";
                context.shows.Add(Ttk);
                Show Nick = new Show();
                Nick.Title = "Nick @ Night";
                Nick.Details = "Just laugh a little bit.";
                Nick.Image = "/Content/NickAtNight.PNG";
                Nick.SoundCloudLink = "https://soundcloud.com/nick-argall-493249104";
                Nick.NavImage = "~/Content/greenDrip.png";
                context.shows.Add(Nick);
                context.SaveChanges();
            }
            if (!roleManager.RoleExists("Fan"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Fan";
                roleManager.Create(role);
            }


        }

        private void SeedEvents()
        {
            Event e = new Event();
            e.Subject = "Test Event and Map";
            e.Description = "This is a test";
            e.Start = DateTime.Now;
            e.EventTime = "Now";
            e.Details = "Testing, testing";
            e.StreetAddress = "3720 Forest Heights Drive";
            e.City = "Eau Claire";
            e.State = "WI";
            e.Zipcode = "54701";

            context.events.Add(e);
            context.SaveChanges();
        }
    }
}
