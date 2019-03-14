using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models
{
    public class RootObject
    {
        public List<object> html_attributions { get; set; }
        public GoogleRestaurantDetails result { get; set; }
        public string status { get; set; }
    }
}