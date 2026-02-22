using Smart_Event_Management_and_Ticketing_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Event_Management_and_Ticketing_System.Models
{
    [Table("CATEGORY", Schema = "SYSTEM")] 
    public class Category
    {
        [Key]
        [Column("CATEGORY_ID")]
        public decimal CategoryId { get; set; }

        [Column("CATEGORY_TYPE")]
        public string? CategoryType { get; set; }

        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        public virtual ICollection<Event>? Events { get; set; }
    }
}
