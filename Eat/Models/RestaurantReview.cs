namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RestaurantReview
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RestaurantReview()
        {
            RestaurantReviews1 = new HashSet<RestaurantReview>();
        }

        public int Id { get; set; }

        public int? ParentReviewId { get; set; }

        public int RestaurantId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Review { get; set; }

        public int Stars { get; set; }

        public DateTime Time { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantReview> RestaurantReviews1 { get; set; }

        public virtual RestaurantReview RestaurantReview1 { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
