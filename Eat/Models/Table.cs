namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Table()
        {
            OrderTables = new HashSet<OrderTable>();
        }

        public int Id { get; set; }

        public int RestaurantId { get; set; }

        [StringLength(247)]
        public string PathTableFoto { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int? MaxNumberPeople { get; set; }

        public int? MinNumberPeople { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public double Prepayment { get; set; }

        public bool Availability { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTable> OrderTables { get; set; }
        
        public int? NumberTables { get; set; } 

        public int? GroupId { get; set; }

        public virtual Table Group { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
