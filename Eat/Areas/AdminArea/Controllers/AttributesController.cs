using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eat.Models;
using Attribute = Eat.Models.Attribute;

namespace Eat.Areas.AdminArea.Controllers
{
    public class AttributesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/Attributes
        public ActionResult Index()
        {
            return View(db.Attributes.ToList());
        }

        // GET: AdminArea/Attributes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // GET: AdminArea/Attributes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminArea/Attributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Attributes.Add(attribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attribute);
        }

        // GET: AdminArea/Attributes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // POST: AdminArea/Attributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attribute);
        }

        // GET: AdminArea/Attributes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // POST: AdminArea/Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attribute attribute = db.Attributes.Find(id);
            db.Attributes.Remove(attribute);
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
