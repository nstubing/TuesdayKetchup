using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuesdayKetchup.Models;
using System.Net.Mail;

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

        public ActionResult Contact(string message)
        {
            ViewBag.Message = message;

            return View();
        }

        [HttpPost]
        public ActionResult Contact([Bind(Include = "Id,Subject,Message")] Email emailEntered)
        {
            ApplicationUser user = context.Users.Find(User.Identity.GetUserId());
            Email email = new Email { Id = emailEntered.Id, Subject = emailEntered.Subject, Message = emailEntered.Message };
            email.FanEmail = user.Email;
            email.RecipientEmail = "thetuesdayketchup@gmail.com";
            email.SenderEmail = "GravyTrainFanEmails@gmail.com";
            email.SenderPassword = "poiuyt1!";
            
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(email.SenderEmail);
            mail.To.Add(email.RecipientEmail);
            mail.Subject = email.Subject ;
            mail.Body = email.Message + "\n\nReply to: " + email.FanEmail;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(email.SenderEmail, email.SenderPassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            return View("Index");
        }

        public ActionResult Ketchup()
        {
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
            int? ratingSum = 0;
            EpisodeViewModel episodeVM = new EpisodeViewModel();
            episodeVM.episode = context.episodes.Where(e => e.Id == id).FirstOrDefault();
            episodeVM.comments = context.comments.Include("ApplicationUser").Where(c => c.EpisodeId == id).ToList();
            foreach(Comment comment in episodeVM.comments)
            {
                ratingSum += comment.Rating;
            }
            episodeVM.rating = ratingSum / episodeVM.comments.Count;
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