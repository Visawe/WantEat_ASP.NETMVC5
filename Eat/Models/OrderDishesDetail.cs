namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDishesDetail
    {
        public int Id { get; set; }

        public int OrderDishesId { get; set; }

        public int DishId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public int? PriceCurrencyId { get; set; }

        public virtual Currency PriceCurrency { get; set; }

        public virtual Dish Dish { get; set; }

        public virtual OrdersDish OrdersDish { get; set; }
    }
}
