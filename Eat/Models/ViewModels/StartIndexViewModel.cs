using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class StartIndexViewModel
    {
        public IList<Restaurant> TopRestaurants { get; set; }
        public IList<Order> LastOrders { get; set; }
        public IList<GoogleReview> LastReviews { get; set; }
        public IList<Restaurant> NewRestaurants { get; set; }
    }
}