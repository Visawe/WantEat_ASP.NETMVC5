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
    public class DishesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminArea/Dishes
        public ActionResult Index()
        {
            var dishes = db.Dishes.Include(d => d.MenuSection).Include(d => d.PriceCurrency).Include(d => d.Restaurant).Include(d => d.WeightUnit);
            return View(dishes.ToList());
        }

        // GET: AdminArea/Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // GET: AdminArea/Dishes/Create
        public ActionResult Create()
        {
            ViewBag.MenuSectionId = new SelectList(db.MenuSections, "Id", "Section");
            ViewBag.PriceCurrencyId = new SelectList(db.Currencies, "Id", "Type");
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            ViewBag.WeightUnitId = new SelectList(db.Units, "Id", "Type");
            return View();
        }

        // POST: AdminArea/Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RestaurantId,MenuSectionId,Name,PathDishFoto,WeightAll,WeightPart,WeightUnitId,Price,PriceCurrencyId,Description,Recommendation,Availability")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Dishes.Add(dish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuSectionId = new SelectList(db.MenuSections, "Id", "Section", dish.MenuSectionId);
            ViewBag.PriceCurrencyId = new SelectList(db.Currencies, "Id", "Type", dish.PriceCurrencyId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", dish.RestaurantId);
            ViewBag.WeightUnitId = new SelectList(db.Units, "Id", "Type", dish.WeightUnitId);
            return View(dish);
        }

        // GET: AdminArea/Dishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuSectionId = new SelectList(db.MenuSections, "Id", "Section", dish.MenuSectionId);
            ViewBag.PriceCurrencyId = new SelectList(db.Currencies, "Id", "Type", dish.PriceCurrencyId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", dish.RestaurantId);
            ViewBag.WeightUnitId = new SelectList(db.Units, "Id", "Type", dish.WeightUnitId);
            return View(dish);
        }

        // POST: AdminArea/Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RestaurantId,MenuSectionId,Name,PathDishFoto,WeightAll,WeightPart,WeightUnitId,Price,PriceCurrencyId,Description,Recommendation,Availability")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuSectionId = new SelectList(db.MenuSections, "Id", "Section", dish.MenuSectionId);
            ViewBag.PriceCurrencyId = new SelectList(db.Currencies, "Id", "Type", dish.PriceCurrencyId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", dish.RestaurantId);
            ViewBag.WeightUnitId = new SelectList(db.Units, "Id", "Type", dish.WeightUnitId);
            return View(dish);
        }

        // GET: AdminArea/Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: AdminArea/Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
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
