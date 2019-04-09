using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class OrderTableDetail
    {
        public int TableId { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int NumberPeople { get; set; }

        public DateTime OrderTimeFrom { get; set; }

        public DateTime? OrderTimeTo { get; set; }

        public double? NeedPrepayment { get; set; }

        public int? PaymentTypeId { get; set; }

        [Required]
        [StringLength(128)]
        public string LastChangedUserId { get; set; }

    }
}