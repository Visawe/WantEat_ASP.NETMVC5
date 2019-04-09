using Eat.Models;
using Eat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eat.Controllers
{
    public class RestaurantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Restaurant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowGalleryRestaurant(int id)
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

        public ActionResult ShowAllTable(int id)
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
            RestaurantDetailsSearchViewModel model = new RestaurantDetailsSearchViewModel();
            ModelSearch search = new ModelSearch();
            model.Search = search;
            model.Restaurant = restaurant;
            model.TablesBySearch = restaurant.Tables.ToList();
            return View("~/Views/Search/ViewDetailsSearchRestaurant.cshtml", model);
        }

        public ActionResult ShowAllDish (int id)
        {
            return View();
        }
    }
}