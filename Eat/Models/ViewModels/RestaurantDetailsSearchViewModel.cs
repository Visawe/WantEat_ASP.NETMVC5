using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class RestaurantDetailsSearchViewModel
    {
        public Restaurant Restaurant { get; set; }
        public ModelSearch Search { get; set; }
        public IList<Table> TablesBySearch { get; set; }
    }
}