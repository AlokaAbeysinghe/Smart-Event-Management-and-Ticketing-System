using Microsoft.EntityFrameworkCore;
using Smart_Event_Management_and_Ticketing_System.Models;

namespace Smart_Event_Management_and_Ticketing_System.Data
{
    public class EventSystemContext : DbContext
    {
        public EventSystemContext(DbContextOptions<EventSystemContext> options)
            : base(options)
        {
        }

        // Add or update these 9 DbSet properties
        public DbSet<Member> Members { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Category> Categories { get; set; }  
        public DbSet<SeatType> SeatTypes { get; set; }   
        public DbSet<Payment> Payments { get; set; }     
        public DbSet<Ticket> Tickets { get; set; }       
        public DbSet<Review> Reviews { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This ensures Entity Framework treats decimal IDs correctly for Oracle
            base.OnModelCreating(modelBuilder);
        }
    }
}
