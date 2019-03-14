namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RestaurantPhoto")]
    public partial class RestaurantPhoto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(247)]
        public string Path { get; set; }

        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
