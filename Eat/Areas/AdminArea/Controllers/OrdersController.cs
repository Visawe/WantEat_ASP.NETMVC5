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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Restaurant).Include(o => o.User).Include(o => o.User1).Include(o => o.User2);
            return View(orders.ToList());
        }

        // GET: AdminArea/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: AdminArea/Orders/Create
        public ActionResult Create()
        {
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar");
            ViewBag.ClientUserId = new SelectList(db.Users, "Id", "Avatar");
            ViewBag.ManagerUserId = new SelectList(db.Users, "Id", "Avatar");
            return View();
        }

        // POST: AdminArea/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RestaurantId,ClientUserId,ManagerUserId,LastChangedUserId,TimeCreated,TimeChanged,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", order.RestaurantId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", order.LastChangedUserId);
            ViewBag.ClientUserId = new SelectList(db.Users, "Id", "Avatar", order.ClientUserId);
            ViewBag.ManagerUserId = new SelectList(db.Users, "Id", "Avatar", order.ManagerUserId);
            return View(order);
        }

        // GET: AdminArea/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", order.RestaurantId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", order.LastChangedUserId);
            ViewBag.ClientUserId = new SelectList(db.Users, "Id", "Avatar", order.ClientUserId);
            ViewBag.ManagerUserId = new SelectList(db.Users, "Id", "Avatar", order.ManagerUserId);
            return View(order);
        }

        // POST: AdminArea/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RestaurantId,ClientUserId,ManagerUserId,LastChangedUserId,TimeCreated,TimeChanged,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", order.RestaurantId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", order.LastChangedUserId);
            ViewBag.ClientUserId = new SelectList(db.Users, "Id", "Avatar", order.ClientUserId);
            ViewBag.ManagerUserId = new SelectList(db.Users, "Id", "Avatar", order.ManagerUserId);
            return View(order);
        }

        // GET: AdminArea/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: AdminArea/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
