using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class OrderTableDetailModelView
    {
        public int IdTable { get; set; }
        public Table Table { get; set; }
        public Restaurant Restaurant { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public ushort? NumberPersons { get; set; }
    }
}