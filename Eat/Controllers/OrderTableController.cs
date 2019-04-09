using Eat.Models;
using Eat.Models.ViewModels;
using Microsoft.AspNet.Identity;
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


            int dayWeekSearch = -1;
            //string date = String.Empty;
            DateTime dateTimeSearch = DateTime.Now;
            if(model.Date == null)
            {
                model.Date = dateTimeSearch;
            }
            if (model.Date != null)
            {
                dayWeekSearch = (int)model.Date.Value.DayOfWeek;
                //date = model.Date.Value.Date.ToString("dd/MM/yyyy");
                //date = model.Date.Value.Date.ToString("MM/dd/yyyy");
                if (model.Time != null)
                {
                    dateTimeSearch = (DateTime)(model.Date + model.Time);
                }
            }

            var tableOrders = table.OrderTables.Where(ot => ot.OrderTimeFrom.Date == model.Date.Value.Date).OrderBy(o => o.OrderTimeFrom);
            var WorkRest = restaurant.WorkSchedules.Where(ws => ws.DayOfWeek == 8 || ws.DayOfWeek == dayWeekSearch).OrderBy(o => o.TimeFrom);
            IList<TimeSpan> ListWorkDateTime = new List<TimeSpan>();
            TimeSpan timeByStep = WorkRest.First().TimeFrom;
            TimeSpan minutes15 = TimeSpan.Parse("00:15");
            foreach(WorkSchedule workSchedule in WorkRest)
            {
                while (timeByStep.Add(minutes15) <= workSchedule.TimeTo)
                {
                    timeByStep = timeByStep.Add(minutes15);
                    ListWorkDateTime.Add(timeByStep);
                }
            }
            IList<TimeSpan> ListOrderDateTime = new List<TimeSpan>();
            if (tableOrders.Count() != 0)
            {
                TimeSpan timeOrderByStep = tableOrders.First().OrderTimeFrom.TimeOfDay;
                ListOrderDateTime.Add(timeOrderByStep);
                foreach (OrderTable orderTable in tableOrders)
                {
                    while (timeOrderByStep.Add(minutes15) < orderTable.OrderTimeTo.Value.TimeOfDay)
                    {
                        timeOrderByStep = timeOrderByStep.Add(minutes15);
                        ListOrderDateTime.Add(timeOrderByStep);
                    }
                }
            }
            IEnumerable<TimeSpan> timeSpans1 = ListWorkDateTime;
            IEnumerable<TimeSpan> timeSpans2 = ListOrderDateTime.AsEnumerable();
            model.MyTimeSpans = timeSpans1.Except(timeSpans2).ToList();
            TimeSpan maxTime = WorkRest.Last().TimeTo;
            if (model.Time != null)
            {
                foreach (OrderTable item in tableOrders)
                {
                    if (item.OrderTimeFrom.TimeOfDay > model.Time)
                    {
                        maxTime = item.OrderTimeFrom.TimeOfDay;
                        break;
                    }
                }
            }
            model.TimeTo = maxTime -(model.Time ?? TimeSpan.Parse("00:00"));
            model.DateTimeTo = (DateTime)(model.Date + (model.Time ?? TimeSpan.Parse("00:00")) + model.TimeTo);
            if(model.NumberPersons == null)
            {
                model.NumberPersons = (ushort?)table.MinNumberPeople;
            }
            return View(model);
        }

        public ActionResult GetFreeTimeSpan(int IdTable, DateTime Date)
        {
            Table table = db.Tables.Find(IdTable);
            if (table == null)
            {
                return HttpNotFound();
            }
            Restaurant restaurant = db.Restaurants.Find(table.RestaurantId);
           
            int dayWeekSearch = -1;
            string date = String.Empty;
            DateTime dateTimeSearch = DateTime.Now;
            var tableOrders = table.OrderTables.Where(ot => ot.OrderTimeFrom.Date == Date.Date).OrderBy(o => o.OrderTimeFrom);
            var WorkRest = restaurant.WorkSchedules.Where(ws => ws.DayOfWeek == 8 || ws.DayOfWeek == dayWeekSearch).OrderBy(o => o.TimeFrom);
            IList<TimeSpan> ListWorkDateTime = new List<TimeSpan>();
            TimeSpan timeByStep = WorkRest.First().TimeFrom;
            TimeSpan minutes15 = TimeSpan.Parse("00:15");
            foreach (WorkSchedule workSchedule in WorkRest)
            {
                while (timeByStep.Add(minutes15) <= workSchedule.TimeTo)
                {
                    timeByStep = timeByStep.Add(minutes15);
                    ListWorkDateTime.Add(timeByStep);
                }
            }
            IList<TimeSpan> ListOrderDateTime = new List<TimeSpan>();
            if (tableOrders.Count() != 0)
            {
                TimeSpan timeOrderByStep = tableOrders.First().OrderTimeFrom.TimeOfDay;
                ListOrderDateTime.Add(timeOrderByStep);
                foreach (OrderTable orderTable in tableOrders)
                {
                    while (timeOrderByStep.Add(minutes15) < orderTable.OrderTimeTo.Value.TimeOfDay)
                    {
                        timeOrderByStep = timeOrderByStep.Add(minutes15);
                        ListOrderDateTime.Add(timeOrderByStep);
                    }
                }
            }
            IEnumerable<TimeSpan> timeSpans1 = ListWorkDateTime;
            IEnumerable<TimeSpan> timeSpans2 = ListOrderDateTime.AsEnumerable();
            IList <TimeSpan> result = timeSpans1.Except(timeSpans2).ToList();
            return PartialView(result);
        }



        [Authorize]
        [HttpPost]
        public ActionResult OrderTableDetail(OrderTableModelViewPost orderModel)
        {
            if(orderModel.TimeBooking == null && orderModel.DateTimeBooking == null)
            {
                OrderTableDetailModelView oldModel = new OrderTableDetailModelView();
                oldModel.IdTable = orderModel.TableId;
                oldModel.NumberPersons = (ushort?)orderModel.NumberPersons;
                oldModel.Date = orderModel.Date;
                ViewBag.Message = "Необходимо ввести данные для поиска";
                return RedirectToAction("OrderTableDetail", "OrderTable", oldModel);
            }
            Order order = new Order();
            Table table = db.Tables.Find(orderModel.TableId);
            Restaurant restaurant = db.Restaurants.Find(table.Restaurant.Id);
            order.RestaurantId = restaurant.Id;
            order.ClientUserId = User.Identity.GetUserId();
            order.LastChangedUserId = order.ClientUserId;
            order.TimeChanged = DateTime.Now;
            order.TimeCreated = DateTime.Now;
            order.Status = "New";
            db.Orders.Add(order);
            db.SaveChanges();
            OrderTable orderTable = new OrderTable();
            orderTable.OrderId = order.Id;
            orderTable.TableId = orderModel.TableId;
            orderTable.Status = "Request";
            orderTable.Description = orderModel.Description;
            orderTable.NumberPeople = orderModel.NumberPersons;
            orderTable.OrderTimeFrom = orderModel.Date + orderModel.Time;
            if(orderModel.DateTimeBooking != null)
            {
                orderTable.OrderTimeTo = orderModel.DateTimeBooking;
                orderTable.BookingMinutes = (orderTable.OrderTimeTo.Value.Subtract(orderTable.OrderTimeFrom)).Minutes;
            }
            if(orderModel.Time != null)
            {
                orderTable.OrderTimeTo = orderModel.Date + orderModel.Time + orderModel.TimeBooking;
                orderTable.BookingMinutes = (int)orderModel.TimeBooking.Value.TotalMinutes;
            }
            
            orderTable.NeedPrepayment = orderModel.Prepayment;
            if(orderTable.NeedPrepayment == 0)
            {
                orderTable.StatusPayment = "fully paid";
            }
            else
            {
                orderTable.StatusPayment = "not paid";
                //orderTable.PaymentTypeId
            }
            orderTable.LastChangedUserId = User.Identity.GetUserId();
            orderTable.TimeChanged = DateTime.Now;
            orderTable.TimeCreated = DateTime.Now;
            db.OrderTables.Add(orderTable);
            int result = db.SaveChanges();
            if (result <= 0)
            {
                ViewBag.Message = "All Bad";
            }
            else
            {
                ViewBag.Message = "Success";
            }
            return View("~/Views/OrderTable/SuccessOrderTable.cshtml", orderTable);
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