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
    public class NationalCuisinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/NationalCuisines
        public ActionResult Index()
        {
            return View(db.NationalCuisines.ToList());
        }

        // GET: AdminArea/NationalCuisines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalCuisine nationalCuisine = db.NationalCuisines.Find(id);
            if (nationalCuisine == null)
            {
                return HttpNotFound();
            }
            return View(nationalCuisine);
        }

        // GET: AdminArea/NationalCuisines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminArea/NationalCuisines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] NationalCuisine nationalCuisine)
        {
            if (ModelState.IsValid)
            {
                db.NationalCuisines.Add(nationalCuisine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nationalCuisine);
        }

        // GET: AdminArea/NationalCuisines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalCuisine nationalCuisine = db.NationalCuisines.Find(id);
            if (nationalCuisine == null)
            {
                return HttpNotFound();
            }
            return View(nationalCuisine);
        }

        // POST: AdminArea/NationalCuisines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] NationalCuisine nationalCuisine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationalCuisine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nationalCuisine);
        }

        // GET: AdminArea/NationalCuisines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalCuisine nationalCuisine = db.NationalCuisines.Find(id);
            if (nationalCuisine == null)
            {
                return HttpNotFound();
            }
            return View(nationalCuisine);
        }

        // POST: AdminArea/NationalCuisines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NationalCuisine nationalCuisine = db.NationalCuisines.Find(id);
            db.NationalCuisines.Remove(nationalCuisine);
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
