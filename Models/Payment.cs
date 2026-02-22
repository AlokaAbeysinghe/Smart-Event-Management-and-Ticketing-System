using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("PAYMENT", Schema = "SYSTEM")]
    public class Payment
    {
        [Key]
        [Column("PAYMENT_ID")]
        public decimal PaymentId { get; set; }

        [Column("AMOUNT")]
        public decimal? Amount { get; set; }

        [Column("PAYMENT_METHOD")]
        public string? PaymentMethod { get; set; }

        [Column("TRANSACTION_DATE")]
        public DateTime? TransactionDate { get; set; }

        [Column("BOOKING_ID")]
        public decimal BookingId { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking? Booking { get; set; }
    }
}
