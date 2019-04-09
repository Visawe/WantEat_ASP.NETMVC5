using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class FilterRestaurantsViewModel
    {
        public IList<int> Restaurants { get; set; }
        public IList<int> NationalCuisinesList { get; set; }
        public IList<int> TypeFoodsList { get; set; }
        public IList<int> TargetAudiencesList { get; set; }
        public IList<int> AttributesList { get; set; }
        public ModelSearch ModelSearch { get; set; }
        public string TypeSearch { get; set; }


        public FilterRestaurantsViewModel()
        {
            Restaurants = new List<int>();
            NationalCuisinesList = new List<int>();
            TypeFoodsList = new List<int>();
            TargetAudiencesList = new List<int>();
            AttributesList = new List<int>();
        }
    }
}