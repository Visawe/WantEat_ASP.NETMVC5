using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class ViewDetailsSearchModel
    {
        public int IdRest { get; set; }
        //public ModelSearch Search { get; set; }
        public string Place { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public ushort? NumberPersons { get; set; }
        public string TextSearch { get; set; }
        public string SearchTarget { get; set; }
        public string TypeSearch { get; set; }
    }
}