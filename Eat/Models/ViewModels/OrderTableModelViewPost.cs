using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class OrderTableModelViewPost
    {
        public int TableId { get; set; }
        public int NumberPersons { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan? TimeBooking {get; set;}
        public DateTime? DateTimeBooking { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public float Prepayment { get; set; }
    }
}