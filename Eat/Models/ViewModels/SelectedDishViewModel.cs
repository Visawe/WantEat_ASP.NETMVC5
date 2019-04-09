using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class SelectedDishViewModel
    {
        public IDictionary<int,int> SelectedDish { get; set; }
        public DateTime OrderTime { get; set; }
        public ushort  NumberPeople { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public double TotalCost { get; set; }
        public SelectedDishViewModel()
        {
            SelectedDish = new Dictionary<int, int>();

        }
    }
}