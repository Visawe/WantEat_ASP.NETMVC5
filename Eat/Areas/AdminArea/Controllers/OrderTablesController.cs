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
    public class OrderTablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/OrderTables
        public ActionResult Index()
        {
            var orderTables = db.OrderTables.Include(o => o.Order).Include(o => o.PaymentType).Include(o => o.Table).Include(o => o.User);
            return View(orderTables.ToList());
        }

        // GET: AdminArea/OrderTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            return View(orderTable);
        }

        // GET: AdminArea/OrderTables/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "ClientUserId");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type");
            ViewBag.TableId = new SelectList(db.Tables, "Id", "PathTableFoto");
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar");
            return View();
        }

        // POST: AdminArea/OrderTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,TableId,Status,Description,AnswerManager,NumberPeople,OrderTimeFrom,OrderTimeTo,BookingMinutes,NeedPrepayment,ReceivedPrepayment,StatusPayment,PaymentTypeId,LastChangedUserId,TimeCreated,TimeChanged")] OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                db.OrderTables.Add(orderTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "Id", "ClientUserId", orderTable.OrderId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type", orderTable.PaymentTypeId);
            ViewBag.TableId = new SelectList(db.Tables, "Id", "PathTableFoto", orderTable.TableId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", orderTable.LastChangedUserId);
            return View(orderTable);
        }

        // GET: AdminArea/OrderTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "ClientUserId", orderTable.OrderId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type", orderTable.PaymentTypeId);
            ViewBag.TableId = new SelectList(db.Tables, "Id", "PathTableFoto", orderTable.TableId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", orderTable.LastChangedUserId);
            return View(orderTable);
        }

        // POST: AdminArea/OrderTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,TableId,Status,Description,AnswerManager,NumberPeople,OrderTimeFrom,OrderTimeTo,BookingMinutes,NeedPrepayment,ReceivedPrepayment,StatusPayment,PaymentTypeId,LastChangedUserId,TimeCreated,TimeChanged")] OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "ClientUserId", orderTable.OrderId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Type", orderTable.PaymentTypeId);
            ViewBag.TableId = new SelectList(db.Tables, "Id", "PathTableFoto", orderTable.TableId);
            ViewBag.LastChangedUserId = new SelectList(db.Users, "Id", "Avatar", orderTable.LastChangedUserId);
            return View(orderTable);
        }

        // GET: AdminArea/OrderTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            return View(orderTable);
        }

        // POST: AdminArea/OrderTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderTable orderTable = db.OrderTables.Find(id);
            db.OrderTables.Remove(orderTable);
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
