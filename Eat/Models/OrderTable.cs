namespace Eat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderTable")]
    public partial class OrderTable
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int TableId { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string AnswerManager { get; set; }

        public int? NumberPeople { get; set; }

        public DateTime OrderTimeFrom { get; set; }

        public DateTime? OrderTimeTo { get; set; }

        public int? BookingMinutes { get; set; }

        public double? NeedPrepayment { get; set; }

        public double? ReceivedPrepayment { get; set; }

        [Required]
        [StringLength(30)]
        public string StatusPayment { get; set; }

        public int? PaymentTypeId { get; set; }

        [Required]
        [StringLength(128)]
        public string LastChangedUserId { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTime TimeChanged { get; set; }

        public virtual Order Order { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public virtual Table Table { get; set; }
    }
}
