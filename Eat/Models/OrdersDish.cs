namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrdersDish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrdersDish()
        {
            OrderDishesDetails = new HashSet<OrderDishesDetail>();
        }

        public int Id { get; set; }

        public int? OrderId { get; set; }
        public int? OrderTableId { get; set; }

        [Required]
        [StringLength(30)]
        public string DeliveryType { get; set; }

        public double? NeedPrepayment { get; set; }

        public double? ReceivedPrepayment { get; set; }

        public int? PaymentTypeId { get; set; }

        public double? Discount { get; set; }

        public double? TotalCost { get; set; }

        public double? TotalCostAfterDiscount { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string AnswerManager { get; set; }

        public DateTime OrderTime { get; set; }

        public int? NumberPeople { get; set; }

        [StringLength(30)]
        public string LadingNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        [Required]
        [StringLength(30)]
        public string StatusPayment { get; set; }

        [Required]
        [StringLength(128)]
        public string LastChangedUserId { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTime TimeChanged { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDishesDetail> OrderDishesDetails { get; set; }

        public virtual Order Order { get; set; }

        public virtual OrderTable OrderTable { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}
