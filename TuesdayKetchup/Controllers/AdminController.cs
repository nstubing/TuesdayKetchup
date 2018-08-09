﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuesdayKetchup.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TuesdayKetchup.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Home()
        {
            return View();
        }
        public ActionResult ManageHome()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult TextAlert()
        {
            if (User.IsInRole("Admin"))
            {
                var ShowNames = db.shows.Select(s => s.Title);
                ViewBag.ShowNames = ShowNames;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ListEpisodes()
        {
            if(User.IsInRole("Admin"))
            {
                ViewBag.Message = TempData["Message"];
                var episodes = db.episodes.Select(u => u).Include(u => u.Show);
                return View(episodes);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        public ActionResult DeleteEpisode(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var thisEp = db.episodes.FirstOrDefault(e => e.Id == id);
                return View(thisEp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult DeleteEpisode(Episode episode)
        {
            if (User.IsInRole("Admin"))
            {
                var thisEp = db.episodes.FirstOrDefault(e => e.Id == episode.Id);
                db.episodes.Remove(thisEp);
                db.SaveChanges();
                TempData["Message"] = "The episode titled " + thisEp.Title + " has been deleted.";
                return RedirectToAction("ListEpisodes");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult DeleteShow(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var thisShow = db.shows.FirstOrDefault(s => s.Id == id);
                return View(thisShow);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult DeleteShow(Show show)
        {
            var thisShow = db.shows.FirstOrDefault(s => s.Id == show.Id);
            db.shows.Remove(thisShow);
            db.SaveChanges();
            TempData["Message"] = "The show titled " + thisShow.Title + " has been deleted.";
            return RedirectToAction("ListShows");
        }
        [HttpPost]
        public ActionResult TextAlert(TextAlert textAlert, string ShowName)
        {
            var thisShowId = db.shows.FirstOrDefault(s => s.Title == ShowName).Id;
            var signedUpForTexts = db.texts.Where(t => t.ShowId == thisShowId).Include(t=>t.ApplicationUser);
            var PhoneNumbersChar = signedUpForTexts.Select(s => s.ApplicationUser.PhoneNumber).ToList();
            List<string> phoneNumbers = new List<string>();
            foreach(string phoneNumber in PhoneNumbersChar)
            {
                string numberString ="+1"+phoneNumber.ToString();
                phoneNumbers.Add(numberString);
            }
            var newTextAlert = new TextAlert() { EpisodeLink = textAlert.EpisodeLink, Message = textAlert.Message, ShowId = thisShowId };
            string thisMessage = textAlert.EpisodeLink + " " + textAlert.Message;
            string twilioSid = MyKeys.TwilioSid;
            string authToken = MyKeys.TwilioAuth;
            TwilioClient.Init(twilioSid, authToken);
            foreach(string phoneNumber in phoneNumbers)
            {
                var message = MessageResource.Create(
                    body: thisMessage,
                    from: new Twilio.Types.PhoneNumber("+15404411403"),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                    );
            }
            TempData["ShowName"] = ShowName;
            return RedirectToAction("TextSent");
        }

        public ActionResult TextSent()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.ShowName = TempData["ShowName"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult AddEpisode()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.Message = TempData["saved"];
                var ShowNames = db.shows.Select(s => s.Title);
                ViewBag.ShowNames = ShowNames;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddEpisode(Episode episode,string ShowName)
        {
            if (User.IsInRole("Admin"))
            {
                var thisShowId = db.shows.FirstOrDefault(s => s.Title == ShowName).Id;
                Episode thisEpisode = new Episode();
                thisEpisode.Title = episode.Title;
                thisEpisode.Details = episode.Details;
                thisEpisode.SoundCloudLink = episode.SoundCloudLink;
                thisEpisode.ShowId = thisShowId;
                db.episodes.Add(thisEpisode);
                db.SaveChanges();
                TempData["saved"] = "Episode saved succesfully";
                return RedirectToAction("AddEpisode");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult EditEpisode(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var Episode = db.episodes.FirstOrDefault(e => e.Id == id);
                var ShowNames = db.shows.Select(s => s.Title);
                ViewBag.ShowNames = ShowNames;
                return View(Episode);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditEpisode(Episode episode, string ShowName)
        {
            var ShowNames = db.shows.Select(s => s.Title);
            ViewBag.ShowNames = ShowNames;
            ViewBag.Message = "Changes have been saved.";
            var EpisodeToChange = db.episodes.FirstOrDefault(e => e.Id == episode.Id);
            EpisodeToChange.Title = episode.Title;
            EpisodeToChange.Details = episode.Details;
            EpisodeToChange.SoundCloudLink = episode.SoundCloudLink;
            var ShowId = db.shows.FirstOrDefault(s => s.Title == ShowName).Id;
            EpisodeToChange.ShowId = ShowId;
            db.SaveChanges();
            return View();
        }
        public ActionResult ListShows()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.Message = TempData["Message"];
                var shows = db.shows.Select(s => s);
                return View(shows);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult EditShow(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var show = db.shows.FirstOrDefault(s => s.Id == id);
                return View(show);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditShow(Show show, HttpPostedFileBase file)
        {
            var thisShow = db.shows.FirstOrDefault(s => s.Id == show.Id);

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content"), pic);
                file.SaveAs(path);
                thisShow.Image = "/Content/"+pic;
            }
            thisShow.Title = show.Title;
            thisShow.Details = show.Details;
            thisShow.SoundCloudLink = show.SoundCloudLink;
            thisShow.PatreonId = show.PatreonId;
            thisShow.ItunesLink = show.ItunesLink;
            db.SaveChanges();
            ViewBag.Message = "Changes have been saved";
            return View();
        }

        public ActionResult AddAnnouncement()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.Message = TempData["Message"];
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddAnnouncement(string Announcement)
        {
            //HomeInfo firstHomeInfo = new HomeInfo();
            //firstHomeInfo.Announcement = "Welcome to Gravy Train Productions";
            //firstHomeInfo.SliderPic1 = "~/Content/tuesdayKetchupBanner.jpg";
            //firstHomeInfo.SliderPic2 = "~/Content/nicknightbanner.jpg";
            //firstHomeInfo.SliderPic3 = "~/Content/gravytrainBanner.jpg";
            //db.homeInfos.Add(firstHomeInfo);

            var HomeInf = db.homeInfos.Select(h => h).FirstOrDefault();
            HomeInf.Announcement = Announcement;
            db.SaveChanges();
            TempData["Message"] = "Your announcement has been saved to the home page.";
            return RedirectToAction("AddAnnouncement");
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
         if (User.IsInRole("Admin"))
                    {
                        if (file != null)
                        {
                            string pic = System.IO.Path.GetFileName(file.FileName);
                            string path = System.IO.Path.Combine(
                                                   Server.MapPath("~/Content"), pic);
                            file.SaveAs(path);
                        }
                        // after successfully uploading redirect the user
                        return RedirectToAction("actionname", "controller name");
                    }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }
    }

}