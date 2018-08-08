using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuesdayKetchup.Models;

namespace TuesdayKetchup.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Forum
        public ActionResult Index()
        {
            ViewBag.ActiveUserId = User.Identity.GetUserId();
            return View(db.threads.ToList());
        }

        // GET: Forum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            ViewBag.Posts = db.posts.Where(p => p.ThreadId == id).ToList();
            ViewBag.ActiveUserId = User.Identity.GetUserId();
            return View(thread);
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thread);
        }

        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(int id, [Bind(Include = "Id,UserId,ThreadId,Message")] Post post)
        {
            var newPost = new Post { UserId = User.Identity.GetUserId(), ThreadId = id, Message = post.Message };
            newPost.UserName = db.Users.Where(u => u.Id == newPost.UserId).FirstOrDefault().UserName;

            db.posts.Add(newPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FlagPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        public ActionResult FlagPost(int id)
        {
            PostFlag flag = new PostFlag { PostID = id, UserID = User.Identity.GetUserId() };
            PostFlag originalFlag = db.postFlags.Where(p => p.PostID == id && p.UserID != flag.UserID).FirstOrDefault();
            if(originalFlag != null)
            {
                originalFlag.Counter++;
                if(originalFlag.Counter >= 5)
                {
                    originalFlag.IsRemoved = true;
                    Post post = db.posts.Where(p => p.Id == id).FirstOrDefault();
                    db.posts.Remove(post);
                }
                flag.Counter = originalFlag.Counter;
                flag.IsRemoved = originalFlag.IsRemoved;
            }
            else
            {
                flag.Counter = 1;
                flag.IsRemoved = false;
                db.postFlags.Add(flag);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "Id,UserId,UserName,ThreadId,Message")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            Post post = db.posts.Find(id);
            db.posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thread);
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.threads.Find(id);
            db.threads.Remove(thread);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
