using Eat.Models;
using Eat.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Eat.Controllers
{
    public class SearchController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Search(ModelSearch search)
        {    
            if(search.Date!= null && search.Date.Value.Date < DateTime.Now.Date)
            {
                ViewBag.Message = "Необходимо ввести корректные данные для поиска";
                return RedirectToAction("Index", "Home");
            }
            if(search.Date != null && search.Time != null)
            {
                DateTime dateTimeSearch = DateTime.Now;
                dateTimeSearch = (DateTime)(search.Date + search.Time);
                if(dateTimeSearch.AddMinutes(5) <= DateTime.Now)
                {
                    ViewBag.Message = "Необходимо ввести корректные данные для поиска";
                    return RedirectToAction("Index", "Home");
                }
            }
            if(search.Time != null && search.Date == null)
            {
                ViewBag.Message = "Необходимо ввести корректные данные для поиска";
                return RedirectToAction("Index", "Home");
            }
           
        
            //Заполнен город
            if (search.Place != null && search.TextSearch == null)
            {
                return RedirectToAction("ResultRestaurants", search);
            }
            //заполнено поле текста для поиска
            else if (search.TextSearch != null)
            {
                switch (search.SearchTarget)
                {
                    case "Object Name":
                        {
                            return RedirectToAction("ResultRestaurants", search);
                        }

                    case "Dish Name":
                        {                
                            return RedirectToAction("ResultDishes", search);
                        }

                    case "Ingredient":
                        {
                            return RedirectToAction("ResultDishes", search);
                        }

                    default:
                        break;
                }

            }
            //ничего не заполнено
            else
            {
                ViewBag.Message = "Необходимо ввести корректные данные для поиска";
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ResultDishes (ModelSearch search)
        {
            DishResultSearchViewModel model = new DishResultSearchViewModel();
            model.ModelSearch = search;
            switch (search.SearchTarget)
            {
                case "Object Name":
                    {
                        return RedirectToAction("ResultRestaurants", search);
                    }

                case "Dish Name":
                    {
                        if (search.Place == null)
                        {
                            IList<Dish> dishes = (from t in db.Dishes // определяем каждый объект из teams как t
                                                  where (t.Name.ToUpper() == search.TextSearch.ToUpper() || t.Name.ToUpper().Contains(search.TextSearch.ToUpper())) //фильтрация по критерию
                                                  orderby t.Price  // упорядочиваем по возрастанию
                                                  select t).ToList(); // выбираем объект
                            model.Dishes = dishes;
                            model.TypeSearch = "Dish";
                        }
                        else
                        {
                            IList<Dish> dishes = (from t in db.Dishes // определяем каждый объект из teams как t
                                                  where ((t.Restaurant.Address.City == search.Place) && (t.Name.ToUpper() == search.TextSearch.ToUpper() || t.Name.ToUpper().Contains(search.TextSearch.ToUpper()))) //фильтрация по критерию
                                                  orderby t.Price  // упорядочиваем по возрастанию
                                                  select t).ToList(); // выбираем объект
                            model.Dishes = dishes;
                            model.TypeSearch = "DishCity";
                            
                        }
                    }break;

                case "Ingredient":
                    {
                        if (search.Place == null)
                        {
                            IList<Dish> dishes = (from t in db.Dishes // определяем каждый объект из teams как t
                                                  where (t.IngredientTypes.Any(item => item.Type.ToUpper() == search.TextSearch.ToUpper() || item.Type.ToUpper().Contains(search.TextSearch.ToUpper()))) //фильтрация по критерию
                                                  orderby t.Price  // упорядочиваем по возрастанию
                                                  select t).ToList(); // выбираем объект
                          
                            model.Dishes = dishes;
                            model.TypeSearch = "Ingredient";
                        }
                        else
                        {
                            IList<Dish> dishes = (from t in db.Dishes // определяем каждый объект из teams как t
                                                  where ((t.Restaurant.Address.City == search.Place)
                                                  && (t.IngredientTypes.Any(item => item.Type.ToUpper() == search.TextSearch.ToUpper() || item.Type.ToUpper().Contains(search.TextSearch.ToUpper())))) //фильтрация по критерию
                                                  orderby t.Price  // упорядочиваем по возрастанию
                                                  select t).ToList(); // выбираем объект
                            model.Dishes = dishes;
                            model.TypeSearch = "IngredientCity";
                        }
                    }break;

                default:
                    break;
            }
            return View(model);
        }

        public async System.Threading.Tasks.Task<ActionResult> ResultRestaurants(ModelSearch search)
        {
            IList<Restaurant> restaurants = null;
            RestaurantResultSearchViewModel model = new RestaurantResultSearchViewModel { Restaurants = restaurants, ModelSearch = search };
            model.NationalCuisines = db.NationalCuisines.ToList();
            model.TargetAudiences = db.TargetAudiences.ToList();
            model.TypeFoods = db.TypeFoods.ToList();
            model.Attributes = db.Attributes.ToList();
            //Заполнен город
            if (search.Place != null && search.TextSearch == null)
            {
                //введен только город для поиска (время/дата == null)
                if (search.Date == null
                    && search.Time == null)
                {
                    if (search.NumberPersons == null || search.NumberPersons == 0)
                    {
                        //возвращаем рестораны только по городу (все)
                        model.Restaurants = (from t in db.Restaurants // определяем каждый объект из teams как t
                                       where (t.Address.City.ToUpper() == search.Place.ToUpper()) //фильтрация по критерию
                                       orderby t.AverageCheckRestaurant  // упорядочиваем по возрастанию
                                       select t).ToList(); // выбираем объект
                        model.TypeSearch = "City";
                    }
                    else
                    {
                        //возвращаем рестораны по городу и числу персон
                        model.Restaurants = (from t in db.Restaurants // определяем каждый объект из teams как t
                                       where (t.Address.City.ToUpper() == search.Place.ToUpper() && t.Tables.Any(m => m.MaxNumberPeople >= search.NumberPersons && search.NumberPersons >= m.MinNumberPeople)) //фильтрация по критерию
                                       orderby t.AverageCheckRestaurant  // упорядочиваем по возрастанию
                                       select t).ToList(); // выбираем объект
                        model.TypeSearch = "CityPerson";
                    }
                                       
                }
                //введен город, время, дата и число персон
                else if (search.Date != null
                    && search.Time != null)
                {
                    int dayWeekSearch = (int)search.Date.Value.DayOfWeek;
                    DateTime dateTimeSearch = (DateTime)(search.Date + search.Time);

                    if (search.NumberPersons == null || search.NumberPersons == 0)
                    {
                        model.Restaurants = (from r in db.Restaurants.ToList()
                                       join ws in db.WorkSchedules on r.Id equals ws.RestaurantId
                                       join t in db.Tables on r.Id equals t.RestaurantId
                                       where (r.Address.City.ToUpper() == search.Place.ToUpper()
                                       && (ws.DayOfWeek == 8 || ws.DayOfWeek == dayWeekSearch)
                                       && (ws.TimeFrom <= search.Time && ws.TimeTo >= search.Time))
                                       select r).Distinct().Intersect((from r in db.Restaurants.ToList()
                                                                       join tt in db.Tables on r.Id equals tt.RestaurantId
                                                                       where !(from t in db.Tables.ToList()
                                                                               join ot in db.OrderTables on t.Id equals ot.TableId
                                                                               where ot.OrderTimeFrom <= dateTimeSearch && ot.OrderTimeTo > dateTimeSearch
                                                                               select t.Id).Contains(tt.Id)
                                                                       select r).Distinct()).ToList();

                        model.TypeSearch = "CityDateTime";
                    }
                    else
                    {
                                        model.Restaurants = (from r in db.Restaurants.ToList()
                                       join ws in db.WorkSchedules on r.Id equals ws.RestaurantId
                                       join t in db.Tables on r.Id equals t.RestaurantId
                                       where (r.Address.City.ToUpper() == search.Place.ToUpper()
                                       && (ws.DayOfWeek == 8 || ws.DayOfWeek == dayWeekSearch)
                                       && (ws.TimeFrom <= search.Time && ws.TimeTo >= search.Time)
                                       && (t.MinNumberPeople <= search.NumberPersons && t.MaxNumberPeople >= search.NumberPersons))
                                       select r).Distinct().Intersect((from r in db.Restaurants.ToList()
                                                                       join tt in db.Tables on r.Id equals tt.RestaurantId
                                                                       where !(from t in db.Tables.ToList()
                                                                               join ot in db.OrderTables on t.Id equals ot.TableId
                                                                               where ot.OrderTimeFrom <= dateTimeSearch &&
                                                                               ot.OrderTimeTo > dateTimeSearch
                                                                               select t.Id).Contains(tt.Id)
                                                                       select r).Distinct()).ToList();
                        model.TypeSearch = "CityDateTimePerson";
                    }                  
                }
                //введен город и дата (время == null)
                else if (search.Date != null
                    && search.Time == null)
                {
                    int dayWeekSearch = (int) search.Date.Value.DayOfWeek;

                    if (search.NumberPersons == null || search.NumberPersons == 0)
                    {
                        model.Restaurants = FindRestaurantsByCityDate(search);
                        model.TypeSearch = "CityDate";

                    }
                    else
                    {
                        var WorkMinutIdRest = (from ws in db.WorkSchedules
                                               join r in db.Restaurants on ws.RestaurantId equals r.Id
                                               join t in db.Tables on r.Id equals t.RestaurantId
                                               where (ws.DayOfWeek == 8 || ws.DayOfWeek == dayWeekSearch)
                                               && r.Address.City.ToUpper() == search.Place.ToUpper()
                                                && (t.MinNumberPeople <= search.NumberPersons && t.MaxNumberPeople >= search.NumberPersons)
                                               group ws by ws.RestaurantId into g
                                               let amount = g.Sum(s => s.WorkMinutes)
                                               select new
                                               {
                                                   IDRest = g.Key,
                                                   SumWorkDay = amount
                                               }).ToList();

                        var BookMinutTableIdRestId = (from ot in db.OrderTables.ToList()
                                                      join t in db.Tables on ot.TableId equals t.Id
                                                      join r in db.Restaurants on t.RestaurantId equals r.Id
                                                      where ot.OrderTimeFrom.Date == DateTime.Parse(search.Date.Value.Date.ToString("dd/MM/yyyy"))
                                                      && r.Address.City.ToUpper() == search.Place.ToUpper()
                                                       && (t.MinNumberPeople <= search.NumberPersons && t.MaxNumberPeople >= search.NumberPersons)
                                                      group ot by new
                                                      {
                                                          ot.Order.RestaurantId,
                                                          ot.TableId,
                                                      } into g
                                                      let amount = g.Sum(s => s.BookingMinutes)
                                                      select new
                                                      {
                                                          IDRest = g.Key.RestaurantId,
                                                          IDTable = g.Key.TableId,
                                                          BookTimeTable = amount
                                                      }).ToList();

                        var idRestFreeTableOnDate = (from WorkMinutIdRestItem in WorkMinutIdRest
                                                     join BookMinutTableItem in BookMinutTableIdRestId
                                                     on WorkMinutIdRestItem.IDRest equals BookMinutTableItem.IDRest into ps
                                                     from BookMinutTableItem in ps.DefaultIfEmpty()
                                                     let FreeTableTime = WorkMinutIdRestItem.SumWorkDay - (BookMinutTableItem == null ? 0 : BookMinutTableItem.BookTimeTable)
                                                     where FreeTableTime == null || FreeTableTime >= 30
                                                     select WorkMinutIdRestItem.IDRest
                                      ).Distinct().ToList();

                        model.Restaurants = (db.Restaurants.Where(r => idRestFreeTableOnDate.Contains(r.Id))).ToList();
                        model.TypeSearch = "CityDatePerson";
                    }                    
                }
                else
                {
                    ViewBag.Message = "Необходимо ввести данные для поиска";
                }
            }
            //заполнено только поле текста для поиска
            else if (search.Place == null && search.TextSearch != null)
            {
                switch (search.SearchTarget)
                {
                    case "Object Name":
                        {
                            //выведем все рестораны
                            if (search.NumberPersons == null || search.NumberPersons == 0)
                            {
                                model.Restaurants = (from t in db.Restaurants // определяем каждый объект из teams как t
                                               where (t.Name.ToUpper() == search.TextSearch.ToUpper() || t.Name.ToUpper().Contains(search.TextSearch.ToUpper())) //фильтрация по критерию
                                               orderby t.AverageCheckRestaurant  // упорядочиваем по возрастанию
                                               select t).ToList(); // выбираем объект
                                model.TypeSearch = "Object";

                            }
                            else
                            {
                                model.Restaurants = (from t in db.Restaurants // определяем каждый объект из teams как t
                                               where (t.Name.ToUpper() == search.TextSearch.ToUpper() 
                                               || t.Name.ToUpper().Contains(search.TextSearch.ToUpper())
                                               && t.Tables.Any(m => m.MaxNumberPeople <= search.NumberPersons && search.NumberPersons >= m.MinNumberPeople)) //фильтрация по критерию
                                               orderby t.AverageCheckRestaurant  // упорядочиваем по возрастанию
                                               select t).ToList(); // выбираем объект  
                                model.TypeSearch = "ObjectPerson";
                            }                           
                        }
                        break;

                    

                    default:
                        break;
                }

            }
            //заролнен город и поле для текста поиска
            else if (search.Place != null && search.TextSearch != null)
            {
                switch (search.SearchTarget)
                {
                    case "Object Name":
                        {
                            //выведем все рестораны
                            if (search.NumberPersons == null || search.NumberPersons == 0)
                            {
                                model.Restaurants = (from t in db.Restaurants // определяем каждый объект из teams как t
                                               where (t.Address.City.ToUpper() == search.Place.ToUpper() && t.Name.ToUpper() == search.TextSearch.ToUpper()
                                               || t.Name.ToUpper().Contains(search.TextSearch.ToUpper())) //фильтрация по критерию
                                               orderby t.AverageCheckRestaurant  // упорядочиваем по возрастанию
                                               select t).ToList(); // выбираем объект
                                model.TypeSearch = "CityObject";

                            }
                            else
                            {
                                model.Restaurants = (from t in db.Restaurants // определяем каждый объект из teams как t
                                               where (t.Address.City.ToUpper() == search.Place.ToUpper()
                                               && (t.Name.ToUpper() == search.TextSearch.ToUpper()
                                               || t.Name.ToUpper().Contains(search.TextSearch.ToUpper())
                                               && t.Tables.Any(m => m.MaxNumberPeople <= search.NumberPersons
                                               && search.NumberPersons >= m.MinNumberPeople))) //фильтрация по критерию
                                               orderby t.AverageCheckRestaurant  // упорядочиваем по возрастанию
                                               select t).ToList(); // выбираем объект
                                model.TypeSearch = "CityObjectPerson";

                            }                            
                        }break;

                    default:
                        break;
                }
            }
            //ничего не заполнено
            else
            {
                ViewBag.Message = "Необходимо ввести данные для поиска";
            }
            
            await UpdateReviewForRestaurantsAsync(model.Restaurants);
            return View(model);
        }

        private static List<Restaurant> FindRestaurantsByCityDate(ModelSearch search)
        {
            List<Restaurant> restaurants;
            int dayWeekSearch = (int)search.Date.Value.DayOfWeek;
            var WorkMinutIdRest = (from ws in db.WorkSchedules
                                   join r in db.Restaurants on ws.RestaurantId equals r.Id
                                   where (ws.DayOfWeek == 8 || ws.DayOfWeek == 3)
                                   && r.Address.City == search.Place
                                   group ws by ws.RestaurantId into g
                                   let amount = g.Sum(s => s.WorkMinutes)
                                   select new
                                   {
                                       IDRest = g.Key,
                                       SumWorkDay = amount
                                   }).ToList();

            var BookMinutTableIdRestId = (from ot in db.OrderTables.ToList()
                                          join t in db.Tables on ot.TableId equals t.Id
                                          join r in db.Restaurants on t.RestaurantId equals r.Id
                                          where ot.OrderTimeFrom.Date == DateTime.Parse(search.Date.Value.Date.ToString("MM/dd/yyyy"))
                                          && r.Address.City == search.Place
                                          group ot by new
                                          {
                                              ot.Order.RestaurantId,
                                              ot.TableId,
                                          } into g
                                          let amount = g.Sum(s => s.BookingMinutes)
                                          select new
                                          {
                                              IDRest = g.Key.RestaurantId,
                                              IDTable = g.Key.TableId,
                                              BookTimeTable = amount
                                          }).ToList();

            var idRestFreeTableOnDate = (from WorkMinutIdRestItem in WorkMinutIdRest
                                         join BookMinutTableItem in BookMinutTableIdRestId
                                         on WorkMinutIdRestItem.IDRest equals BookMinutTableItem.IDRest into ps
                                         from BookMinutTableItem in ps.DefaultIfEmpty()
                                         let FreeTableTime = WorkMinutIdRestItem.SumWorkDay - (BookMinutTableItem == null ? 0 : BookMinutTableItem.BookTimeTable)
                                         where FreeTableTime == null || FreeTableTime >= 30
                                         select WorkMinutIdRestItem.IDRest
                          ).Distinct().ToList();

            restaurants = (db.Restaurants.Where(r => idRestFreeTableOnDate.Contains(r.Id))).ToList();
            return restaurants;
        }

        // IdRest = item.Id, Search = Model.ModelSearch, TypeSearch = Model.TypeSearch
        //public ActionResult ViewDetailsSearch (int IdRest, ModelSearch Search, string TypeSearch)
        public ActionResult ViewDetailsSearchRestaurant(ViewDetailsSearchModel model)
        {
            Restaurant restaurant = db.Restaurants.Find(model.IdRest);
            IList<Table> tables = null;
            int dayWeekSearch = -1;
            string date = String.Empty;
            DateTime dateTimeSearch = DateTime.Now;
            if (model.Date != null)
            {
                dayWeekSearch = (int)model.Date.Value.DayOfWeek;
                date = model.Date.Value.Date.ToString("dd/MM/yyyy");
                //date = model.Date.Value.Date.ToString("MM/dd/yyyy");
                if (model.Time != null)
                {
                    dateTimeSearch = (DateTime)(model.Date + model.Time);
                }
            }
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            switch (model.TypeSearch)
            {
                case "City":
                case "Object":
                case "CityObject":
                    {
                        return RedirectToAction("Details", "Restaurant", new { id = model.IdRest});
                    }
                case "CityPerson":
                case "ObjectPerson":
                case "CityObjectPerson":
                    {
                        tables = restaurant.Tables.Where(t => t.MaxNumberPeople >= model.NumberPersons
                        && t.MinNumberPeople <= model.NumberPersons && t.Availability == true).ToList();
                    }
                    break;
                case "CityDateTime":
                    {
                        // where ot.OrderTimeFrom <= dateTimeSearch && ot.OrderTimeTo > dateTimeSearch
                        tables = restaurant.Tables.Where(t => !(t.OrderTables.Where(ot => ot.OrderTimeFrom <= dateTimeSearch && ot.OrderTimeTo > dateTimeSearch).Select(s => s.TableId)).Contains(t.Id) 
                        && t.Availability == true).ToList();
                    }break;
                case "CityDate":
                    {
                        int workMinute = restaurant.WorkSchedules.Where(ws => ws.DayOfWeek == 8 || ws.DayOfWeek == dayWeekSearch).Sum(s => s.WorkMinutes);
                        tables = restaurant.Tables.Where(t => (workMinute -
                        t.OrderTables.Where(ot => ot.OrderTimeFrom.Date == model.Date.Value.Date).Sum(m => m.BookingMinutes)) > 30
                        && t.Availability == true).ToList();
                    }break;
                case "CityDatePerson":
                    {
                        int workMinute = restaurant.WorkSchedules.Where(ws => ws.DayOfWeek == 8 || ws.DayOfWeek == dayWeekSearch).Sum(s => s.WorkMinutes);
                        tables = restaurant.Tables.Where(t => (workMinute -
                        t.OrderTables.Where(ot => ot.OrderTimeFrom.Date == model.Date.Value.Date).Sum(m => m.BookingMinutes)) > 30
                        && t.Availability == true
                        && t.MaxNumberPeople >= model.NumberPersons
                        && t.MinNumberPeople <= model.NumberPersons).ToList();
                    }break;
                case "CityDateTimePerson":
                    {
                        tables = restaurant.Tables.Where(t => 
                        !(t.OrderTables.Where(ot => ot.OrderTimeFrom <= dateTimeSearch && ot.OrderTimeTo > dateTimeSearch).Select(s => s.TableId)).Contains(t.Id)
                        && t.MaxNumberPeople >= model.NumberPersons
                        && t.MinNumberPeople <= model.NumberPersons && t.Availability == true).ToList();
                    }break;
                default:
                    break;
            }
            ModelSearch search = new ModelSearch() { Place = model.Place, Date = model.Date, Time = model.Time, NumberPersons = model.NumberPersons };
            RestaurantDetailsSearchViewModel restaurantDetailsSearchViewModel = new RestaurantDetailsSearchViewModel()
            {
                Restaurant = restaurant,
                Search = search,
                TablesBySearch = tables
            };
            return View(restaurantDetailsSearchViewModel);
        }

        private async Task UpdateReviewForRestaurantsAsync(IEnumerable<Restaurant> restaurants)
        {
            if (restaurants != null)
            {
                List<Restaurant> rest = restaurants.ToList();
                foreach (Restaurant restaurant in rest)
                {
                    if (restaurant.GoogleRestaurantDetails != null)
                    {
                        if ((restaurant.GoogleRestaurantDetails.DateTimeLastUpdate == null)
                            //|| (restaurant.GoogleRestaurantDetails.DateTimeLastUpdate.Value.Date != DateTime.Now.Date)
                            )
                        {
                            await GetReview(restaurant);
                        }
                    }
                }
            }
        }

        private static async System.Threading.Tasks.Task GetReview(Restaurant restaurant)
        {
            using (var client = new HttpClient())
            {
                StringBuilder partAdr1 = new StringBuilder("https://maps.googleapis.com/maps/api/place/details/json?placeid=");
                partAdr1.Append(restaurant.GoogleRestaurantDetails.Place_id.ToString());
                partAdr1.Append("&fields=name,place_id,rating,review&language=ru&key=AIzaSyByiiKmBiFwlKq3ySCf38vkzXx80KIUOWc");

                HttpResponseMessage response = await client.GetAsync(partAdr1.ToString());
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(responseBody);

                if (rootObject != null)
                {
                    restaurant.GoogleRestaurantDetails.Name = rootObject.result.Name;
                    restaurant.GoogleRestaurantDetails.Rating = rootObject.result.Rating;
                    foreach (GoogleReview review in rootObject.result.GoogleReviews)
                    {
                        if (!restaurant.GoogleRestaurantDetails.GoogleReviews.Contains(review))
                        {
                            restaurant.GoogleRestaurantDetails.GoogleReviews.Add(review);
                        }
                    }
                    restaurant.GoogleRestaurantDetails.DateTimeLastUpdate = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }


        [HttpPost]
        public JsonResult GetByFilter(FilterRestaurantsViewModel model)
        {
            IList<Restaurant> restaurants = null;
            if(model.Restaurants.Count > 0)
            {
                restaurants = db.Restaurants.Where(n => model.Restaurants.Contains(n.Id)).ToList();
                
                if (model.NationalCuisinesList.Count > 0)
                {
                    restaurants = restaurants.Where(n => n.NationalCuisines.Select(nc => nc.Id).Intersect(model.NationalCuisinesList).ToList().Count  == model.NationalCuisinesList.Count).ToList();
                }
                if (model.TargetAudiencesList.Count > 0)
                {
                    restaurants = restaurants.Where(n => n.TargetAudiences.Select(nc => nc.Id).Intersect(model.TargetAudiencesList).ToList().Count == model.TargetAudiencesList.Count).ToList();
                }
                if (model.AttributesList.Count > 0)
                {
                    restaurants = restaurants.Where(n => n.Attributes.Select(nc => nc.Id)
                    .Intersect(model.AttributesList).ToList().Count == model.AttributesList.Count).ToList();
                }
                if (model.TypeFoodsList.Count > 0)
                {
                    restaurants = restaurants.Where(n => n.TypeFoods.Select(nc => nc.Id)
                    .Intersect(model.TypeFoodsList).ToList().Count == model.TypeFoodsList.Count).ToList();
                }

                string viewContent = ConvertViewToString("GetByFilter", restaurants);
                return Json(new { PartialView = viewContent });
            }
            string viewContent2= ConvertViewToString("GetByFilter", restaurants);
            return Json("error");
        }

        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
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
