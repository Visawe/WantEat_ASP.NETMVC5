using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class DishResultSearchViewModel
    {
        public IList<Dish> Dishes { get; set; }
        public ModelSearch ModelSearch { get; set; }
        public string TypeSearch { get; set; }
    }
}