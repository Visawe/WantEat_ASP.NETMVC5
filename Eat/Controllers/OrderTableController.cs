using Eat.Models;
using Eat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eat.Controllers
{
    public class OrderTableController : Controller
    {

        private static ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult OrderTableDetail(OrderTableDetailModelView model)
        {
            Table table = db.Tables.Find(model.IdTable);
            if (table == null)
            {
                return HttpNotFound();
            }
            Restaurant restaurant = db.Restaurants.Find(table.RestaurantId);
            model.Table = table;
            model.Restaurant = restaurant;

            return View(model);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}