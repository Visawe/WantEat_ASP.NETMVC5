namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dish()
        {
            OrderDishesDetails = new HashSet<OrderDishesDetail>();
            Users = new HashSet<ApplicationUser>();
            IngredientTypes = new HashSet<IngredientType>();
        }

        public int Id { get; set; }      

        public int RestaurantId { get; set; }

        public int? MenuSectionId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(247)]
        public string PathDishFoto { get; set; }

        public int? WeightAll { get; set; }

        [StringLength(100)]
        public string WeightPart { get; set; }

        public int? WeightUnitId { get; set; }

        public virtual Unit WeightUnit { get; set; }

        public double? Price { get; set; }

        public int? PriceCurrencyId { get; set; }

        public virtual Currency PriceCurrency { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int? Recommendation { get; set; }

        public bool Availability { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual MenuSection MenuSection { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDishesDetail> OrderDishesDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUser> Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngredientType> IngredientTypes { get; set; }
    }
}
