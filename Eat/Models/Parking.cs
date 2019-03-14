namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Parking")]
    public partial class Parking
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public bool OwnParking { get; set; }

        public int OwnParkingPlaces { get; set; }

        public bool NotOwnParking { get; set; }

        public int NotOwnParkingPlaces { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
