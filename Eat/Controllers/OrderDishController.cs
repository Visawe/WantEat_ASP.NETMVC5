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
    public class OrderDishController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDish
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderDishToTable (int idOrder, int idRest, int idOrderTable)
        {
            if (idRest == null || idOrder == null || idOrderTable == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(idRest);
            IList<Dish> dishes = restaurant.Dishes.ToList();
            Order order = db.Orders.Find(idOrder);
            OrderTable orderTable = db.OrderTables.Find(idOrderTable);
            if (restaurant == null || order == null|| orderTable == null)
            {
                return HttpNotFound();
            }
            IEnumerable<IGrouping<MenuSection, Dish>> groupByMenuSection = restaurant.Dishes.GroupBy(t => t.MenuSection);
            OrderDishToTableGetModelView model = new OrderDishToTableGetModelView();
            model.Order = order;
            model.OrderTable = orderTable;
            model.GroupByMenuSection = groupByMenuSection;
            return View(model);
        }

        [HttpPost]
        public ActionResult OrderDishToTable (SelectedDishViewModel model)
        {

            return View("~/Views/OrderDish/OrderDishToTableResult.cshtml", model.SelectedDish);
        }
    }
}