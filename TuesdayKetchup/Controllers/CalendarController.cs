using TuesdayKetchup.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace TuesdayKetchup.Controllers
{
    public class CalendarController : Controller
    {
        #region Index method  

        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult CalendarIndex()

        {
            var events = db.events.ToList();
            ViewBag.Events = events;
            var startDates =
                (from u in db.events
                 where u.Start != null
                 select u.Start).First();
            ViewBag.StartDates = startDates;
            var details =
                (from u in db.events
                 where u.Subject != null
                 select u.Subject).First();
            ViewBag.Details = details;

            return View();
        }

        #endregion


        public JsonResult GetEvents()
        {

            {
                var events = db.events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult EventList()
        {
            ViewBag.Message = TempData["Message"];
            var Events = db.events.Select(e => e);
            return View(Events);
        }
        public ActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent([Bind(Include = "Start, End, Description, Subject, Details, Image, StreetAddress, City, State, Zipcode")]Event eventEntered)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.events.Add(eventEntered);
                    db.SaveChanges();
                    return RedirectToAction("CalendarIndex");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(eventEntered);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event eventFound = db.events.Find(id);
            if (eventFound == null)
            {
                return HttpNotFound();
            }
            var eventToMap = db.events.Find(eventFound.Id);
            ViewBag.Image = eventFound.Image;
            ViewBag.Address = eventFound.StreetAddress;
            ViewBag.City = eventFound.City;
            ViewBag.State = eventFound.State;
            ViewBag.ZipCode = eventFound.Zipcode;
            ViewBag.APIKey = MyKeys.GOOGLEAPIKEY;
            return View(eventFound);
        }
        public ActionResult EditEvent(int id)
        {
            var myEvent = db.events.FirstOrDefault(e => e.Id == id);
            return View(myEvent);
        }
        [HttpPost]
        public ActionResult EditEvent(Event thisEvent, HttpPostedFileBase file)
        {
            var myEvent = db.events.FirstOrDefault(e => e.Id == thisEvent.Id);
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content"), pic);
                file.SaveAs(path);
                myEvent.Image = "/Content/" + pic;
            }
            myEvent.Subject = thisEvent.Subject;
            myEvent.Description = thisEvent.Description;
            myEvent.Start = thisEvent.Start;
            myEvent.EventTime = thisEvent.EventTime;
            myEvent.Details = thisEvent.Details;
            myEvent.City = thisEvent.City;
            myEvent.StreetAddress = thisEvent.StreetAddress;
            myEvent.State = thisEvent.State;
            myEvent.Zipcode = thisEvent.Zipcode;
            ViewBag.Message = "Changes have been saved successful.";
            db.SaveChanges();
            return View();
        }
        public ActionResult DeleteEvent(int id)
        {
            var thisEvent = db.events.FirstOrDefault(e => e.Id == id);
            return View(thisEvent);
        }
        [HttpPost]
        public ActionResult DeleteEvent(Event thisEvent)
        {
            var deleteEvent = db.events.FirstOrDefault(e => e.Id == thisEvent.Id);
            db.events.Remove(deleteEvent);
            db.SaveChanges();
            TempData["Message"] = "Your event has been deleted.";
            return RedirectToAction("EventList");
        }
    }
}