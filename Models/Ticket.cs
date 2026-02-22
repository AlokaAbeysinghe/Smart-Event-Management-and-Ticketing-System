using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("TICKET", Schema = "SYSTEM")]
    public class Ticket
    {
        [Key]
        [Column("TICKET_ID")]
        public decimal TicketId { get; set; }

        [Column("SEAT_NUMBER")]
        public string? SeatNumber { get; set; }

        [Column("STATUS")]
        public string? Status { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
