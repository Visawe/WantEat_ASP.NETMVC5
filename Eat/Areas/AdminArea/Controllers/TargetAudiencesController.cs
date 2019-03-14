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
    public class TargetAudiencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/TargetAudiences
        public ActionResult Index()
        {
            return View(db.TargetAudiences.ToList());
        }

        // GET: AdminArea/TargetAudiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetAudience targetAudience = db.TargetAudiences.Find(id);
            if (targetAudience == null)
            {
                return HttpNotFound();
            }
            return View(targetAudience);
        }

        // GET: AdminArea/TargetAudiences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminArea/TargetAudiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] TargetAudience targetAudience)
        {
            if (ModelState.IsValid)
            {
                db.TargetAudiences.Add(targetAudience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(targetAudience);
        }

        // GET: AdminArea/TargetAudiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetAudience targetAudience = db.TargetAudiences.Find(id);
            if (targetAudience == null)
            {
                return HttpNotFound();
            }
            return View(targetAudience);
        }

        // POST: AdminArea/TargetAudiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] TargetAudience targetAudience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(targetAudience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(targetAudience);
        }

        // GET: AdminArea/TargetAudiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetAudience targetAudience = db.TargetAudiences.Find(id);
            if (targetAudience == null)
            {
                return HttpNotFound();
            }
            return View(targetAudience);
        }

        // POST: AdminArea/TargetAudiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TargetAudience targetAudience = db.TargetAudiences.Find(id);
            db.TargetAudiences.Remove(targetAudience);
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
