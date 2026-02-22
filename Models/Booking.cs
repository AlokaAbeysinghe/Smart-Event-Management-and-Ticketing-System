using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("BOOKING", Schema = "SYSTEM")]
    public class Booking
    {
        [Key]
        [Column("BOOKING_ID")]
        public decimal BookingId { get; set; }

        [Column("QUANTITY")]
        public decimal? Quantity { get; set; }

        [Column("MEMBER_ID")]
        public decimal MemberId { get; set; }

        [Column("EVENT_ID")]
        public decimal EventId { get; set; }

        [Column("SEAT_TYPE_ID")]
        public decimal? SeatTypeId { get; set; }

        [Column("TICKET_ID")]
        public decimal? TicketId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member? Member { get; set; }

        [ForeignKey("EventId")]
        public virtual Event? Event { get; set; }

        [ForeignKey("SeatTypeId")]
        public virtual SeatType? SeatType { get; set; }

        [ForeignKey("TicketId")]
        public virtual Ticket? Ticket { get; set; }

        public virtual Payment? Payment { get; set; }
    }
}