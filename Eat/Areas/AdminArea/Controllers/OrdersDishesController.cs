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
    public class OrdersDishesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/OrdersDishes
        public ActionResult Index()
        {
            var ordersDishes = db.OrdersDishes.Include(o => o.Order).Include(o => o.PaymentType).Include(o => o.User);
            return View(ordersDishes.ToList());
        }

        // GET: AdminArea/OrdersDishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDish ordersDish = db.OrdersDishes.Find(id);
            if (ordersDish == null)
            {
                return HttpNotFound();
            }
            return View(ordersDish);
        }

        // GET: AdminArea/OrdersDishes/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "ClientUserId");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type");
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar");
            return View();
        }

        // POST: AdminArea/OrdersDishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,DeliveryType,NeedPrepayment,ReceivedPrepayment,PaymentTypeId,Discount,TotalCost,TotalCostAfterDiscount,Description,AnswerManager,OrderTime,NumberPeople,LadingNumber,Status,StatusPayment,LastChangedUserId,TimeCreated,TimeChanged")] OrdersDish ordersDish)
        {
            if (ModelState.IsValid)
            {
                db.OrdersDishes.Add(ordersDish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "Id", "ClientUserId", ordersDish.OrderId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type", ordersDish.PaymentTypeId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", ordersDish.LastChangedUserId);
            return View(ordersDish);
        }

        // GET: AdminArea/OrdersDishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDish ordersDish = db.OrdersDishes.Find(id);
            if (ordersDish == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "ClientUserId", ordersDish.OrderId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type", ordersDish.PaymentTypeId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", ordersDish.LastChangedUserId);
            return View(ordersDish);
        }

        // POST: AdminArea/OrdersDishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,DeliveryType,NeedPrepayment,ReceivedPrepayment,PaymentTypeId,Discount,TotalCost,TotalCostAfterDiscount,Description,AnswerManager,OrderTime,NumberPeople,LadingNumber,Status,StatusPayment,LastChangedUserId,TimeCreated,TimeChanged")] OrdersDish ordersDish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordersDish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "ClientUserId", ordersDish.OrderId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type", ordersDish.PaymentTypeId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", ordersDish.LastChangedUserId);
            return View(ordersDish);
        }

        // GET: AdminArea/OrdersDishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDish ordersDish = db.OrdersDishes.Find(id);
            if (ordersDish == null)
            {
                return HttpNotFound();
            }
            return View(ordersDish);
        }

        // POST: AdminArea/OrdersDishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdersDish ordersDish = db.OrdersDishes.Find(id);
            db.OrdersDishes.Remove(ordersDish);
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
