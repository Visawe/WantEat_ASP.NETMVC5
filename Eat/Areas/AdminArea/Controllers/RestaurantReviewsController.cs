using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eat.Models;

namespace Eat.Areas.AdminArea.Controllers
{
    public class RestaurantReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/RestaurantReviews
        public ActionResult Index()
        {
            var restaurantReviews = db.RestaurantReviews.Include(r => r.Restaurant).Include(r => r.RestaurantReview1).Include(r => r.User);
            return View(restaurantReviews.ToList());
        }

        // GET: AdminArea/RestaurantReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview restaurantReview = db.RestaurantReviews.Find(id);
            if (restaurantReview == null)
            {
                return HttpNotFound();
            }
            return View(restaurantReview);
        }

        // GET: AdminArea/RestaurantReviews/Create
        public ActionResult Create()
        {
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            ViewBag.ParentReviewId = new SelectList(db.RestaurantReviews, "Id", "Review");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Avatar");
            return View();
        }

        // POST: AdminArea/RestaurantReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ParentReviewId,RestaurantId,Review,Stars,Time,UserId")] RestaurantReview restaurantReview)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantReviews.Add(restaurantReview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurantReview.RestaurantId);
            ViewBag.ParentReviewId = new SelectList(db.RestaurantReviews, "Id", "Review", restaurantReview.ParentReviewId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Avatar", restaurantReview.UserId);
            return View(restaurantReview);
        }

        // GET: AdminArea/RestaurantReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview restaurantReview = db.RestaurantReviews.Find(id);
            if (restaurantReview == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurantReview.RestaurantId);
            ViewBag.ParentReviewId = new SelectList(db.RestaurantReviews, "Id", "Review", restaurantReview.ParentReviewId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Avatar", restaurantReview.UserId);
            return View(restaurantReview);
        }

        // POST: AdminArea/RestaurantReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParentReviewId,RestaurantId,Review,Stars,Time,UserId")] RestaurantReview restaurantReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurantReview.RestaurantId);
            ViewBag.ParentReviewId = new SelectList(db.RestaurantReviews, "Id", "Review", restaurantReview.ParentReviewId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Avatar", restaurantReview.UserId);
            return View(restaurantReview);
        }

        // GET: AdminArea/RestaurantReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview restaurantReview = db.RestaurantReviews.Find(id);
            if (restaurantReview == null)
            {
                return HttpNotFound();
            }
            return View(restaurantReview);
        }

        // POST: AdminArea/RestaurantReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantReview restaurantReview = db.RestaurantReviews.Find(id);
            db.RestaurantReviews.Remove(restaurantReview);
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
