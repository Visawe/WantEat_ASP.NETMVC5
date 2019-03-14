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
    public class ContactTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/ContactTypes
        public ActionResult Index()
        {
            return View(db.ContactTypes.ToList());
        }

        // GET: AdminArea/ContactTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // GET: AdminArea/ContactTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminArea/ContactTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                db.ContactTypes.Add(contactType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactType);
        }

        // GET: AdminArea/ContactTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // POST: AdminArea/ContactTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactType);
        }

        // GET: AdminArea/ContactTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactType contactType = db.ContactTypes.Find(id);
            if (contactType == null)
            {
                return HttpNotFound();
            }
            return View(contactType);
        }

        // POST: AdminArea/ContactTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactType contactType = db.ContactTypes.Find(id);
            db.ContactTypes.Remove(contactType);
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
