using Eat.Models;
using Eat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eat.Areas.AdminArea.Controllers
{
    public class RestaurantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminArea/Restaurant
        public ActionResult Index()
        {
                IList<Restaurant> restaurants = db.Restaurants.Include(d => d.Address).ToList();
                return View(restaurants);
        }

        // GET: AdminArea/Restaurant/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        public ActionResult RestaurantPhotoList (int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: AdminArea/Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminArea/Restaurant/Create
        [HttpPost]
        public ActionResult Create(RestaurantAdsressViewModel model)
        {
            try
            {
                Restaurant restaurant = model.Restaurant;
                Address address = model.Address;


                if (ModelState.IsValidField("Address"))
                {
                    db.Addresses.Add(address);
                    db.SaveChanges();
                    restaurant.Address = address;
                    restaurant.AddressId = address.Id;

                    string fileName = Path.GetFileNameWithoutExtension(restaurant.ImageFile.FileName);
                    string extention = Path.GetExtension(restaurant.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    restaurant.PathMainFoto = "~/Images/RestaurantFotos/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/RestaurantFotos/"), fileName);
                    restaurant.ImageFile.SaveAs(fileName);
                    if (ModelState.IsValidField("Restaurant"))
                    {
                        db.Restaurants.Add(restaurant);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: AdminArea/Restaurant/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: AdminArea/Restaurant/Edit/5
        [HttpPost]
        public ActionResult Edit(Restaurant restaurant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(restaurant).State = EntityState.Modified;
                    db.SaveChanges();
                }

                    return RedirectToAction("Details", new { id = restaurant.Id } );
            }
            catch
            {
                return View(restaurant);
            }
        }


        public ActionResult EditAttributes(Restaurant restaurant)
        {
            restaurant.Attributes.ToList();
            return View(restaurant);
        }

        public ActionResult AddAttribute(Restaurant restaurant)
        {
            return PartialView("AddAttribute",restaurant );
        }

        [HttpPost]
        public ActionResult AddAttribute(Models.Attribute attribute)
        {

            db.Attributes.Add(attribute);
            db.SaveChanges();
            return RedirectToAction("EditAttributes");
        }

        [HttpPost]
        public ActionResult AddAttributeToRestaurant(int id, String attribute)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            bool exist = restaurant.Attributes.Any(x => x.Type == attribute);
            if(!exist)
            {
                Models.Attribute NewAttribute = new Models.Attribute() { Type = attribute };
                db.SaveChanges();
                restaurant.Attributes.Add(NewAttribute);
                db.SaveChanges();
                return Json(new { status = "Success", message = "Success" });
            }
            return Json(new { status = "No", message = "No" });
        }

        public ActionResult AutocompleteSearchAttribute(string term)
        {
            List<Models.Attribute> attributes = db.Attributes.ToList();
            var models = attributes.Where(a => a.Type.Contains(term))
                            .Select(a => new { value = a.Type })
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        // GET: AdminArea/Restaurant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminArea/Restaurant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
