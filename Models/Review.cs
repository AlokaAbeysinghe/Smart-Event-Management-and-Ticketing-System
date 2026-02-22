using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("REVIEW", Schema = "SYSTEM")]
    public class Review
    {
        [Key]
        [Column("REVIEW_ID")]
        public decimal ReviewId { get; set; }

        [Column("COMMENTS")]
        public string? Comments { get; set; }

        [Column("RATING")]
        public decimal? Rating { get; set; }

        [Column("MEMBER_ID")]
        public decimal? MemberId { get; set; }

        [Column("EVENT_ID")]
        public decimal? EventId { get; set; }

        [Column("INQUIRY_ID")]
        public decimal? InquiryId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member? Member { get; set; }

        [ForeignKey("EventId")]
        public virtual Event? Event { get; set; }
    }
}