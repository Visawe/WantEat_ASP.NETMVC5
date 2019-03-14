namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CurrencyRate")]
    public partial class CurrencyRate
    {
        public int Id { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; }
    }
}
