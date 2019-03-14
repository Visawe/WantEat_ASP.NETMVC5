namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkSchedule
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public int DayOfWeek { get; set; }

        public TimeSpan TimeFrom { get; set; }

        public TimeSpan TimeTo { get; set; }

        public int WorkMinutes { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
