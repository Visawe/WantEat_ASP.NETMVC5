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
    public class TablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/Tables
        public ActionResult Index()
        {
            var tables = db.Tables.Include(t => t.Group).Include(t => t.Restaurant);
            return View(tables.ToList());
        }

        // GET: AdminArea/Tables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: AdminArea/Tables/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Tables, "Id", "PathTableFoto");
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            return View();
        }

        // POST: AdminArea/Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RestaurantId,PathTableFoto,Name,MaxNumberPeople,MinNumberPeople,Description,Prepayment,Availability,NumberTables,GroupId")] Table table)
        {
            if (ModelState.IsValid)
            {
                db.Tables.Add(table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Tables, "Id", "PathTableFoto", table.GroupId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", table.RestaurantId);
            return View(table);
        }

        // GET: AdminArea/Tables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Tables, "Id", "PathTableFoto", table.GroupId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", table.RestaurantId);
            return View(table);
        }

        // POST: AdminArea/Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RestaurantId,PathTableFoto,Name,MaxNumberPeople,MinNumberPeople,Description,Prepayment,Availability,NumberTables,GroupId")] Table table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Tables, "Id", "PathTableFoto", table.GroupId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", table.RestaurantId);
            return View(table);
        }

        // GET: AdminArea/Tables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: AdminArea/Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table table = db.Tables.Find(id);
            db.Tables.Remove(table);
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
