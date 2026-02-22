using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("EVENT", Schema = "SYSTEM")]
    public class Event
    {
        [Key]
        [Column("EVENT_ID")]
        public decimal EventId { get; set; }

        [Column("EVENTNAME")]
        public string? EventName { get; set; }

        [Column("EVENTDATE")]
        public DateTime? EventDate { get; set; }

        [Column("VENUE")]
        public string? Venue { get; set; }

        [Column("PRICE")]
        public decimal? Price { get; set; }

        [Column("AVAILABILITY")]
        public string? Availability { get; set; }

        [Column("CATEGORY_ID")]
        public decimal? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [Column("INQUIRY_ID")]
        public decimal? InquiryId { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}