using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class RestaurantResultSearchViewModel
    {
        public IList<Restaurant> Restaurants { get; set; }
        public ModelSearch ModelSearch { get; set; }
        public string TypeSearch { get; set; }
        public IList<NationalCuisine> NationalCuisines { get; set; }
        public IList<TargetAudience> TargetAudiences { get; set; }
        public IList<Attribute> Attributes { get; set; }
        public IList<TypeFood> TypeFoods { get; set; }
    }
}