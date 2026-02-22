using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("MEMBER", Schema = "SYSTEM")]
    public class Member
    {
        [Key]
        [Column("MEMBER_ID")]
        public decimal MemberId { get; set; }

        [Column("FULLNAME")]
        public string? Fullname { get; set; }

        [Column("EMAIL")]
        public string? Email { get; set; }

        [Column("PASSWORD")]
        public string? Password { get; set; }

        [Column("INQUIRY_ID")]
        public decimal? InquiryId { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }

        public virtual ICollection<Inquiry>? Inquiries { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }
    }
}