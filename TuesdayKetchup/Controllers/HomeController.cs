using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuesdayKetchup.Models;
using System.Net.Mail;
using System.Net;

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
        public ActionResult AddComment(string CommentString, string UserId, int EpisodeId, int? Rating)
        {
            Comment comment = new Comment();
            comment.Message = CommentString;
            comment.UserId = UserId;
            comment.EpisodeId = EpisodeId;
            comment.Rating = Rating;
            context.comments.Add(comment);
            context.SaveChanges();
            return RedirectToAction("Ketchup");
        }

        public ActionResult FlagComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = context.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost]
        public ActionResult FlagComment(int id)
        {
            CommentFlag flag = new CommentFlag { CommentID = id, UserID = User.Identity.GetUserId() };
            CommentFlag originalFlag = context.commentFlags.Where(p => p.CommentID == id && p.UserID != flag.UserID).FirstOrDefault();
            if (originalFlag != null)
            {
                originalFlag.Counter++;
                if (originalFlag.Counter >= 5)
                {
                    originalFlag.IsRemoved = true;
                    Comment comment = context.comments.Where(p => p.Id == id).FirstOrDefault();
                    context.comments.Remove(comment);
                }
                flag.Counter = originalFlag.Counter;
                flag.IsRemoved = originalFlag.IsRemoved;
            }
            else
            {
                flag.Counter = 1;
                flag.IsRemoved = false;
                context.commentFlags.Add(flag);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}