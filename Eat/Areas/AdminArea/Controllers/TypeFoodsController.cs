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
    public class TypeFoodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/TypeFoods
        public ActionResult Index()
        {
            return View(db.TypeFoods.ToList());
        }

        // GET: AdminArea/TypeFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeFood typeFood = db.TypeFoods.Find(id);
            if (typeFood == null)
            {
                return HttpNotFound();
            }
            return View(typeFood);
        }

        // GET: AdminArea/TypeFoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminArea/TypeFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] TypeFood typeFood)
        {
            if (ModelState.IsValid)
            {
                db.TypeFoods.Add(typeFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeFood);
        }

        // GET: AdminArea/TypeFoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeFood typeFood = db.TypeFoods.Find(id);
            if (typeFood == null)
            {
                return HttpNotFound();
            }
            return View(typeFood);
        }

        // POST: AdminArea/TypeFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] TypeFood typeFood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeFood);
        }

        // GET: AdminArea/TypeFoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeFood typeFood = db.TypeFoods.Find(id);
            if (typeFood == null)
            {
                return HttpNotFound();
            }
            return View(typeFood);
        }

        // POST: AdminArea/TypeFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeFood typeFood = db.TypeFoods.Find(id);
            db.TypeFoods.Remove(typeFood);
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
