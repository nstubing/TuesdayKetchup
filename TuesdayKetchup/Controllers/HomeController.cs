using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuesdayKetchup.Models;

namespace TuesdayKetchup.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult GetCalendarIndex()
        {
            return RedirectToAction("GetCalendarIndex", "Calendar");
        }
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Home", "Admin");
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Ketchup()
        {
            var Show = context.shows.FirstOrDefault(s => s.Title == "The Tuesday Ketchup");
            var ShowId = Show.Id;
            var ShowDetails = Show.Details;
            ViewBag.ShowDetails = ShowDetails;
            var Episodes = context.episodes.OrderByDescending(e => e.ShowId == ShowId);
            var latestShowLink = Episodes.FirstOrDefault().SoundCloudLink;
            var latestShowDetails = Episodes.FirstOrDefault().Details;
            ViewBag.Details = latestShowDetails;
            string showUrl = "https://w.soundcloud.com/player/?url=" + latestShowLink;
            ViewBag.ShowUrl = showUrl;
            var previousShows = Episodes.Skip(1).ToList();
            ViewBag.PreviousShows = previousShows;
            int EpisodeId = GetMostRecentEpisodeId();
            List<Comment> episodeComments = context.comments.Where(c => c.EpisodeId == EpisodeId).ToList();
            return View();
        }

        private int GetMostRecentEpisodeId()
        {
            //Can we do this by date?
            return 1;
        }
        public ActionResult NickAtNight()
        {
            return View();
        }
    }
}