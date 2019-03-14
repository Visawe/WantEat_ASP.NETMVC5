namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Restaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurant()
        {
            Contacts = new HashSet<Contact>();
            Dishes = new HashSet<Dish>();
            Orders = new HashSet<Order>();
            Parkings = new HashSet<Parking>();
            RestaurantPhotoes = new HashSet<RestaurantPhoto>();
            RestaurantReviews = new HashSet<RestaurantReview>();
            Tables = new HashSet<Table>();
            Users = new HashSet<ApplicationUser>();
            WorkSchedules = new HashSet<WorkSchedule>();
            Attributes = new HashSet<Attribute>();
            NationalCuisines = new HashSet<NationalCuisine>();
            PaymentTypes = new HashSet<PaymentType>();
            TargetAudiences = new HashSet<TargetAudience>();
            TypeFoods = new HashSet<TypeFood>();
        }

        public int Id { get; set; }

        public int AddressId { get; set; }

        public int? GoogleRestaurantDetailsId { get; set; }

        public virtual GoogleRestaurantDetails GoogleRestaurantDetails { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(247)]
        public string PathMainFoto { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [StringLength(3500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string DressCode { get; set; }

        [StringLength(500)]
        public string Slogan { get; set; }

        public double? AverageCheckRestaurant { get; set; }

        public int? AverageCheckRestaurantCurrencyId { get; set; }

        public virtual Currency AverageCheckRestaurantCurrency { get; set; }

        public double? AverageCheckOrder { get; set; }

        public double? Rating { get; set; }

        public string PathGoogleMap { get; set; }

        public string PathYoutube { get; set; }

        public DateTime? VerificationDate { get; set; }

        public virtual Address Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> Contacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dish> Dishes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parking> Parkings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantPhoto> RestaurantPhotoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantReview> RestaurantReviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table> Tables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUser> Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attribute> Attributes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NationalCuisine> NationalCuisines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentType> PaymentTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TargetAudience> TargetAudiences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TypeFood> TypeFoods { get; set; }
    }
}
