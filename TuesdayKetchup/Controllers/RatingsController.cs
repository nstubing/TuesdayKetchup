using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using TuesdayKetchup.Models;

namespace TuesdayKetchup.Controllers
{
    public class RatingsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Ratings
        public ActionResult Index(int episodeId)
        {
            
            Episode episode = db.episodes.Find(episodeId);
            return View(episode);
        }

        [HttpPost]
        public Rating SetRating(int? episodeId, int? star)
        {
            Rating rating = new Rating();
            //rating.Star = star;
            //rating.EpisodeId = episodeId;
            rating.UserId = User.Identity.GetUserId();
            //Episode episode = db.episodes.Find(episodeId);
            //episode.OverallRating = star;
            db.ratings.Add(rating);
            db.SaveChanges();

            rating = db.ratings
                .Include(y => y.Episode)
                .Include(y => y.Episode.ratings)
                .Include(y => y.ApplicationUser)
                .SingleOrDefault(y => rating.Id == rating.Id);
            return (rating);

            //return RedirectToAction("Details", "Shows", new { id = episodeId });
        }
        public PartialViewResult RatingsControl(int episodeID)
        {
            Episode model = db.episodes.Find(episodeID);

            return PartialView("_RatingsControl", model);
        }
    }
}