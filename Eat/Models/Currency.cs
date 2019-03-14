using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eat.Models
{
    public class Currency
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Type { get; set; }

    }
}