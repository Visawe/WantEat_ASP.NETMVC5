using Eat.Models;
using Eat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eat.Controllers
{
    public class HomeController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            StartIndexViewModel model = new StartIndexViewModel();
            model.TopRestaurants = db.Restaurants.OrderByDescending(r => r.GoogleRestaurantDetails.Rating).Take(5).ToList();
            model.LastReviews = db.GoogleReviews.OrderByDescending( gr => gr.Relative_time_description).Take(7).ToList();
            model.LastOrders = db.Orders.OrderByDescending(o => o.TimeCreated).Take(6).ToList();
            model.NewRestaurants = db.Restaurants.OrderByDescending(r => r.Id).Take(6).ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}