using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class OrderDishToTableGetModelView
    {
        public IEnumerable<IGrouping<MenuSection, Dish>> GroupByMenuSection { get; set; }
        public Order Order { get; set; }
        public OrderTable OrderTable { get; set; }
    }
}