using Microsoft.AspNet.Identity;
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
            ViewBag.Announcement = TempData["Announcement"];
            return View();
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
//<<<<<<< HEAD
            ShowViewModel showVM = new ShowViewModel();
            var ShowId = context.shows.FirstOrDefault(s => s.Title == "The Tuesday Ketchup").Id;
            var Episodes = context.episodes.OrderByDescending(e => e.ShowId == ShowId);
            //var latestShowLink = Episodes.FirstOrDefault().SoundCloudLink;
            //string showUrl = "https://w.soundcloud.com/player/?url=" + latestShowLink;
            //ViewBag.ShowUrl = showUrl;
            var previousShows = Episodes.ToList();
            //ViewBag.PreviousShows 
            showVM.episodes = previousShows;
            int EpisodeId = GetMostRecentEpisodeId(ShowId);
            //List<Comment> episodeComments
            showVM.episodeVM.episode = context.episodes.Where(e => e.Id == EpisodeId).FirstOrDefault();
            showVM.episodeVM.comments = context.comments.Include("ApplicationUser").Where(c => c.EpisodeId == EpisodeId).ToList();
            return View(showVM);
//=======
//            var Show = context.shows.FirstOrDefault(s => s.Title == "The Tuesday Ketchup");
//            var ShowId = Show.Id;
//            var ShowDetails = Show.Details;
//            ViewBag.ShowDetails = ShowDetails;
//            var Episodes = context.episodes.OrderByDescending(e => e.ShowId == ShowId);
//            var latestShowLink = Episodes.FirstOrDefault().SoundCloudLink;
//            var latestShowDetails = Episodes.FirstOrDefault().Details;
//            ViewBag.Details = latestShowDetails;
//            string showUrl = "https://w.soundcloud.com/player/?url=" + latestShowLink;
//            ViewBag.ShowUrl = showUrl;
//            var previousShows = Episodes.Skip(1).ToList();
//            ViewBag.PreviousShows = previousShows;
//            int EpisodeId = GetMostRecentEpisodeId();
//            List<Comment> episodeComments = context.comments.Where(c => c.EpisodeId == EpisodeId).ToList();
//            return View();
//>>>>>>> c196f71fb12b2bc63d1afb7ddea4e7eff3cd016c
        }

        private int GetMostRecentEpisodeId(int showId)
        {
            //Can we do this by date?
            var episodes = context.episodes.Where(e => e.ShowId == showId).OrderBy(e => e.Id).Select(e => e.Id).ToList();
            return episodes.Last();
        }

        public PartialViewResult GetEpisodeComments(int id)
        {
            List<Comment> comments = context.comments.Where(c => c.EpisodeId == id).ToList();
            return PartialView("_Comments", comments);
        }
        
        public PartialViewResult GetEpisodePlayer(int id)
        {
            string podcastURL = context.episodes.Where(e => e.Id == id).Select(e => e.SoundCloudLink).FirstOrDefault();
            podcastURL = "https://w.soundcloud.com/player/?url=" + podcastURL;
            return PartialView("_EpisodePlayer", podcastURL);
        }
        public ActionResult NickAtNight()
        {
            return View();
        }

        public PartialViewResult GetEpisodePartial(int id)
        {
            EpisodeViewModel episodeVM = new EpisodeViewModel();
            episodeVM.episode = context.episodes.Where(e => e.Id == id).FirstOrDefault();
            episodeVM.comments = context.comments.Include("ApplicationUser").Where(c => c.EpisodeId == id).ToList();
            return PartialView("_EpisodePartial", episodeVM);
        }

        [HttpPost]
        public ActionResult AddComment(string CommentString, string UserId, int EpisodeId)
        {
            Comment comment = new Comment();
            comment.Message = CommentString;
            comment.UserId = UserId;
            comment.EpisodeId = EpisodeId;
            context.comments.Add(comment);
            context.SaveChanges();
            return View("Ketchup");
        }
    }
}