using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("INQUIRY", Schema = "SYSTEM")]
    public class Inquiry
    {
        [Key]
        [Column("INQUIRY_ID")]
        public decimal InquiryId { get; set; }

        [Column("FULLNAME")]
        public string? Fullname { get; set; }

        [Column("EMAIL")]
        public string? Email { get; set; }

        [Column("MESSAGE")]
        public string? Message { get; set; }

        [Column("MEMBER_TYPE")]
        public string? MemberType { get; set; }

        [Column("MEMBER_ID")]
        public decimal? MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member? Member { get; set; }
    }
}