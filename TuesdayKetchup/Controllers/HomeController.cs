using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuesdayKetchup.Models;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;

namespace TuesdayKetchup.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult GetCalendarIndex()
        {
            return RedirectToAction("GetCalendarIndex", "Calendar");
        }
        public ActionResult Index(Episode episode)
        {
            var HomeInfo = context.homeInfos.Select(h => h).FirstOrDefault();
            if (HomeInfo != null)
            {
                ViewBag.Picture1 = HomeInfo.SliderPic1;
                ViewBag.Picture2 = HomeInfo.SliderPic2;
                ViewBag.Picture3 = HomeInfo.SliderPic3;
            }
            var ReverseEpList = context.episodes.OrderByDescending(e => e.Id);
            var Announce = context.homeInfos.Select(h => h).FirstOrDefault();
            var Show = context.shows.FirstOrDefault(s => s.Title == "The Tuesday Ketchup");
            ViewBag.KetchupLogo = Show.Image;
            var ShowId = Show.Id;

            var KetchupEpisode = ReverseEpList.Where(e => e.ShowId == ShowId).ToList(); ;
            var ShowTwo = context.shows.FirstOrDefault(s => s.Title == "Nick @ Night");
            ViewBag.NickLogo = ShowTwo.Image;
            var ShowIdTwo = ShowTwo.Id;
            var NickEpisode = ReverseEpList.Where(e => e.ShowId == ShowIdTwo).ToList(); ;
            if (KetchupEpisode.Count > 0)
            {
                ViewBag.TuesdayKetchupEp = KetchupEpisode[0];
            }
            if (NickEpisode.Count > 0)
            {
                ViewBag.NickNightEp = NickEpisode[0];
            }
            if (Announce != null)
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

            return RedirectToAction("Index","Home");
        }

        public ActionResult Ketchup()
        {
            ShowViewModel showVM = new ShowViewModel();
            var Show = context.shows.FirstOrDefault(s => s.Title == "The Tuesday Ketchup");
            var ShowId = Show.Id;
            var Episodes = context.episodes.Where(e => e.ShowId == ShowId);
            //var latestShowLink = Episodes.FirstOrDefault().SoundCloudLink;
            //string showUrl = "https://w.soundcloud.com/player/?url=" + latestShowLink;
            //ViewBag.ShowUrl = showUrl;
            var previousShows = Episodes.ToList();
            //ViewBag.PreviousShows
            ViewBag.Img = Show.Image;
            ViewBag.ShowDetails = Show.Details;
            var UserId = User.Identity.GetUserId();
            if (UserId != null)
            {
                var textSignups = context.texts.Where(t => t.UserId == UserId);
                var isSignedUp = textSignups.Where(t => t.ShowId == ShowId).FirstOrDefault();
                if (isSignedUp != null)
                {
                    ViewBag.SignedUp = true;
                }
            }
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

            ViewBag.PatreonSupporters = PatreonMessenger.GetPatrons();
            return View(showVM);
        }

        private int GetMostRecentEpisodeId(int showId)
        {

            
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
            ShowViewModel showVM = new ShowViewModel();
            var Show = context.shows.FirstOrDefault(s => s.Title == "Nick @ Night");
            var ShowId = Show.Id;
            var Episodes = context.episodes.Where(e => e.ShowId == ShowId).ToList();
            //var latestShowLink = Episodes.FirstOrDefault().SoundCloudLink;
            //string showUrl = "https://w.soundcloud.com/player/?url=" + latestShowLink;
            //ViewBag.ShowUrl = showUrl;
            var previousShows = Episodes;
            //ViewBag.PreviousShows
            if(Show.TwitterAccount != null)
            {
                string twitterUrl = Show.TwitterAccount + "?ref_src = twsrc % 5Etfw";
                ViewBag.Twitter = Show.TwitterAccount;
            }
            ViewBag.Itunes = Show.ItunesLink;
            ViewBag.Img = Show.Image;
            ViewBag.ShowDetails = Show.Details;
            var UserId = User.Identity.GetUserId();
            if (UserId != null)
            {
                var textSignups = context.texts.Where(t => t.UserId == UserId);
                var isSignedUp = textSignups.Where(t => t.ShowId == ShowId).FirstOrDefault();
                if (isSignedUp != null)
                {
                    ViewBag.SignedUp = true;
                }
            }
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

            //ViewBag.PatreonSupporters = PatreonMessenger.GetPatrons();
            return View(showVM);
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
            var episode = context.episodes.Where(e => e.Id == episodeId).Include(e=>e.Show).FirstOrDefault();
            if(episode.Show.Title=="The Tuesday Ketchup")
            {
                return RedirectToAction("Ketchup");
            }
            else
            {
                return RedirectToAction("NickAtNight");
            }
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

        [HttpPost]
        public ActionResult TextKetchup()
        {
            var currentUser = User.Identity.GetUserId();
            if(currentUser==null)
            {
                TempData["Message"] = "You must register before you can sign up for text alerts";
                return RedirectToAction("Register", "Account");
            }
            else
            {
                var thisUser = context.Users.FirstOrDefault(u => u.Id == currentUser).PhoneNumber;
                TempData["Number"] = thisUser;
                TempData["Show"] = "Ketchup";
                return RedirectToAction("UpdatePhoneNumber", "Home");
            }
        }
        [HttpPost]
        public ActionResult TextNick()
        {
            var currentUser = User.Identity.GetUserId();
            if (currentUser == null)
            {
                TempData["Message"] = "You must register before you can sign up for text alerts";
                return RedirectToAction("Register", "Account");
            }
            else
            {
                var thisUser = context.Users.FirstOrDefault(u => u.Id == currentUser).PhoneNumber;
                TempData["Number"] = thisUser;
                TempData["Show"] = "Nick";
                return RedirectToAction("UpdatePhoneNumber", "Home");
            }
        }
        public ActionResult UpdatePhoneNumber()
        {
            ViewBag.Number = TempData["Number"];
            ViewBag.Show = TempData["Show"];
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePhoneNumber(ApplicationUser user,string show)
        {
            var ketchupId = context.shows.FirstOrDefault(s => s.Title == "The Tuesday Ketchup").Id;
            var nickId = context.shows.FirstOrDefault(s => s.Title == "Nick @ Night").Id;
            var currentUserId = User.Identity.GetUserId();
            var currentUser = context.Users.Where(u => u.Id == currentUserId).FirstOrDefault();
            currentUser.PhoneNumber = user.PhoneNumber;
            Texts newSignup = new Texts();
            newSignup.UserId = currentUserId;
            if (show =="Ketchup")
            {
                newSignup.ShowId = ketchupId;
                context.texts.Add(newSignup);
                context.SaveChanges();
                return RedirectToAction("Ketchup");
            }
            else
            {
                newSignup.ShowId = nickId;
                context.texts.Add(newSignup);
                context.SaveChanges();
                return RedirectToAction("Nick");
            }
            
        }
    }
}