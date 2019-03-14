namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public int ContactTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Value { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
