using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return View();
        }
        public ActionResult TextAlert()
        {
            var ShowNames = db.shows.Select(s => s.Title);
            ViewBag.ShowNames = ShowNames;
            return View();
        }

        [HttpPost]
        public ActionResult TextAlert(TextAlert textAlert, string ShowName)
        {
            var thisShowId = db.shows.FirstOrDefault(s => s.Title == ShowName).Id;
            var signedUpForTexts = db.texts.Where(t => t.ShowId == thisShowId).Include(t=>t.ApplicationUser);
            var PhoneNumbersChar = signedUpForTexts.SelectMany(s => s.ApplicationUser.PhoneNumber).ToList();
            List<string> phoneNumbers = new List<string>();
            foreach(Char phoneNumber in PhoneNumbersChar)
            {
                string numberString ="1+"+ phoneNumber.ToString();
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
                    from: new Twilio.Types.PhoneNumber("+15017122661"),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                    );
            }
            TempData["ShowName"] = ShowName;
            return RedirectToAction("TextSent");
        }

        public ActionResult TextSent()
        {
            ViewBag.ShowName = TempData["ShowName"].ToString();
            return View();
        }
        public ActionResult AddEpisode()
        {
            ViewBag.Message = TempData["saved"];
            var ShowNames = db.shows.Select(s => s.Title);
            ViewBag.ShowNames = ShowNames;
            return View();
        }
        [HttpPost]
        public ActionResult AddEpisode(Episode episode,string ShowName)
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
    }

}