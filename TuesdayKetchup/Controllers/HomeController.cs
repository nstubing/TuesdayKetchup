﻿using Microsoft.AspNet.Identity;
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
            var Announce = context.homeInfos.Select(h => h).FirstOrDefault();
            if (Announce !=null)
            {
                ViewBag.Announcement = Announce.Announcement;
            }
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
            ShowViewModel showVM = new ShowViewModel();
            var Show = context.shows.FirstOrDefault(s => s.Title == "The Tuesday Ketchup");
            var ShowId = Show.Id;
            var Episodes = context.episodes.OrderByDescending(e => e.ShowId == ShowId);
            //var latestShowLink = Episodes.FirstOrDefault().SoundCloudLink;
            //string showUrl = "https://w.soundcloud.com/player/?url=" + latestShowLink;
            //ViewBag.ShowUrl = showUrl;
            var previousShows = Episodes.ToList();
            //ViewBag.PreviousShows
            ViewBag.Img = Show.Image;
            ViewBag.ShowDetails = Show.Details;
            showVM.episodes = previousShows;
            int EpisodeId = GetMostRecentEpisodeId(ShowId);
            //List<Comment> episodeComments

            List<Rating> ratings = context.Ratings.Where(r => r.EpisodeId == EpisodeId).ToList();
            int ratingSum = 0;
            if (ratings.Count > 0)
            {
                foreach (Rating rating in ratings)
                {
                    ratingSum += rating.Score;
                }
                showVM.episodeVM.rating = ratingSum / ratings.Count;
            }

            showVM.episodeVM.episode = context.episodes.Where(e => e.Id == EpisodeId).FirstOrDefault();
            showVM.episodeVM.comments = context.comments.Include("ApplicationUser").Where(c => c.EpisodeId == EpisodeId).ToList();
            string userId = User.Identity.GetUserId();
            showVM.episodeVM.currentUserRating = context.Ratings.Where(c => c.EpisodeId == EpisodeId).Where(c => c.UserId == userId).Select(c => c.Score).FirstOrDefault();
            return View(showVM);
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
            int ratingSum = 0;
            EpisodeViewModel episodeVM = new EpisodeViewModel();
            episodeVM.episode = context.episodes.Where(e => e.Id == id).FirstOrDefault();
            episodeVM.comments = context.comments.Include("ApplicationUser").Where(c => c.EpisodeId == id).ToList();
            string userId = User.Identity.GetUserId();
            episodeVM.currentUserRating = context.Ratings.Where(c => c.EpisodeId ==id).Where(c => c.UserId == userId).Select(c => c.Score).FirstOrDefault();
            List<Rating> ratings = context.Ratings.Where(r => r.EpisodeId == id).ToList();
            if (ratings.Count > 0)
            {
                foreach (Rating rating in ratings)
                {
                    ratingSum += rating.Score;
                }
                episodeVM.rating = ratingSum / ratings.Count;
            }

            return PartialView("_EpisodePartial", episodeVM);
        }

        [HttpPost]
        public ActionResult AddComment(string CommentString, string UserId, int EpisodeId)
        {
            Comment comment = new Comment() {Message = CommentString, UserId = UserId, EpisodeId = EpisodeId };
            context.comments.Add(comment);
            context.SaveChanges();
            return RedirectToAction("Ketchup");
        }

        [HttpPost]
        public ActionResult AddRating(string userId, int episodeId, int score)
        {
            Rating rating = new Rating() { UserId = userId, EpisodeId = episodeId, Score = score };
            Rating existingRating = context.Ratings.Where(r => r.EpisodeId == episodeId).Where(r => r.UserId == userId).FirstOrDefault();
            if (existingRating != null)
            {
                context.Ratings.Remove(existingRating);
            }
            context.Ratings.Add(rating);
            context.SaveChanges();
            return RedirectToAction("Ketchup");
        }
    }
}