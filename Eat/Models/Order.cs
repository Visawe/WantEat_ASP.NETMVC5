namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrdersDishes = new HashSet<OrdersDish>();
            OrderTables = new HashSet<OrderTable>();
        }

        public int Id { get; set; }

        public int RestaurantId { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientUserId { get; set; }

        [StringLength(128)]
        public string ManagerUserId { get; set; }

        [Required]
        [StringLength(128)]
        public string LastChangedUserId { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTime TimeChanged { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationUser User1 { get; set; }

        public virtual ApplicationUser User2 { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersDish> OrdersDishes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTable> OrderTables { get; set; }
    }
}
