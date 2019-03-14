namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            Restaurants = new HashSet<Restaurant>();
            Users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string Street { get; set; }

        [Required]
        [StringLength(10)]
        public string House { get; set; }

        public int? Apartment { get; set; }

        public int? Entrance { get; set; }

        public int? Floor { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public DbGeography Map { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Restaurant> Restaurants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public string ToStringShort()
        {
            if (Apartment != null)
            {
                return (Country + ", " + City + ", " + Street + ", " + House + ", " + Apartment);
            }
            return (Country + ", " + City + ", " + Street + ", " + House);
        }
    }
}
