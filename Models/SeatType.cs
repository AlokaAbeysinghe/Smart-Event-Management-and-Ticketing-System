using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("SEAT_TYPE", Schema = "SYSTEM")]
    public class SeatType
    {
        [Key]
        [Column("SEAT_TYPE_ID")]
        public decimal SeatTypeId { get; set; }

        [Column("TYPE_NAME")]
        public string? TypeName { get; set; }

        [Column("PRICE_MULTIPLIER")]
        public decimal? PriceMultiplier { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
