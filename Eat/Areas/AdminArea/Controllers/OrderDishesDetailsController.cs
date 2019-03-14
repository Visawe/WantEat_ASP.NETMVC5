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
    public class OrderDishesDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/OrderDishesDetails
        public ActionResult Index()
        {
            var orderDishesDetails = db.OrderDishesDetails.Include(o => o.Dish).Include(o => o.OrdersDish);
            return View(orderDishesDetails.ToList());
        }

        // GET: AdminArea/OrderDishesDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDishesDetail orderDishesDetail = db.OrderDishesDetails.Find(id);
            if (orderDishesDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDishesDetail);
        }

        // GET: AdminArea/OrderDishesDetails/Create
        public ActionResult Create()
        {
            ViewBag.DishId = new SelectList(db.Dishes, "Id", "Name");
            ViewBag.OrderDishesId = new SelectList(db.OrdersDishes, "Id", "DeliveryType");
            return View();
        }

        // POST: AdminArea/OrderDishesDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDishesId,DishId,Quantity,Price")] OrderDishesDetail orderDishesDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDishesDetails.Add(orderDishesDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DishId = new SelectList(db.Dishes, "Id", "Name", orderDishesDetail.DishId);
            ViewBag.OrderDishesId = new SelectList(db.OrdersDishes, "Id", "DeliveryType", orderDishesDetail.OrderDishesId);
            return View(orderDishesDetail);
        }

        // GET: AdminArea/OrderDishesDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDishesDetail orderDishesDetail = db.OrderDishesDetails.Find(id);
            if (orderDishesDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DishId = new SelectList(db.Dishes, "Id", "Name", orderDishesDetail.DishId);
            ViewBag.OrderDishesId = new SelectList(db.OrdersDishes, "Id", "DeliveryType", orderDishesDetail.OrderDishesId);
            return View(orderDishesDetail);
        }

        // POST: AdminArea/OrderDishesDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDishesId,DishId,Quantity,Price")] OrderDishesDetail orderDishesDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDishesDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DishId = new SelectList(db.Dishes, "Id", "Name", orderDishesDetail.DishId);
            ViewBag.OrderDishesId = new SelectList(db.OrdersDishes, "Id", "DeliveryType", orderDishesDetail.OrderDishesId);
            return View(orderDishesDetail);
        }

        // GET: AdminArea/OrderDishesDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDishesDetail orderDishesDetail = db.OrderDishesDetails.Find(id);
            if (orderDishesDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDishesDetail);
        }

        // POST: AdminArea/OrderDishesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDishesDetail orderDishesDetail = db.OrderDishesDetails.Find(id);
            db.OrderDishesDetails.Remove(orderDishesDetail);
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
