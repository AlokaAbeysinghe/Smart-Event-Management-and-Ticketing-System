using Microsoft.EntityFrameworkCore;
using Smart_Event_Management_and_Ticketing_System.Data;
using Smart_Event_Management_and_Ticketing_System.Models;
using System;
using System.Linq;

namespace Smart_Event_Management_and_Ticketing_System.Seeds
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EventSystemContext(
                serviceProvider.GetRequiredService<DbContextOptions<EventSystemContext>>()))
            {
                //  Force Categories Seeding
                if (!context.Categories.Any())
                {
                    Console.WriteLine("Seeding Categories...");
                    context.Categories.AddRange(
                        new Category { CategoryId = 1, CategoryType = "Music", Description = "Live concerts and festivals" },
                        new Category { CategoryId = 2, CategoryType = "Tech", Description = "Programming workshops and seminars" },
                        new Category { CategoryId = 3, CategoryType = "Sports", Description = "Marathons and local matches" }
                    );
                    context.SaveChanges(); 
                }

                //  Seed Seat Types
                if (!context.SeatTypes.Any())
                {
                    Console.WriteLine("Seeding Seat Types...");
                    context.SeatTypes.AddRange(
                        new SeatType { SeatTypeId = 1, TypeName = "Standard", PriceMultiplier = 1.0m },
                        new SeatType { SeatTypeId = 2, TypeName = "VIP", PriceMultiplier = 1.5m },
                        new SeatType { SeatTypeId = 3, TypeName = "VVIP", PriceMultiplier = 2.0m }
                    );
                    context.SaveChanges();
                }

                //  Force Events Seeding
                if (!context.Events.Any())
                {
                    Console.WriteLine("Seeding Events...");
                    context.Events.AddRange(
                        new Event
                        {
                            EventId = 1,
                            EventName = "Grand Symphony Night",
                            EventDate = DateTime.Now.AddDays(10),
                            Venue = "City Opera House",
                            Price = 5000.00m,
                            Availability = "100",
                            CategoryId = 1 // Music
                        },
                        new Event
                        {
                            EventId = 2,
                            EventName = "AI Innovation Summit",
                            EventDate = DateTime.Now.AddDays(20),
                            Venue = "Tech Hub",
                            Price = 2000.00m,
                            Availability = "50",
                            CategoryId = 2 // Tech
                        },
                        new Event
                        {
                            EventId = 3,
                            EventName = "City Charity Marathon",
                            EventDate = DateTime.Now.AddDays(30),
                            Venue = "Main Street Plaza",
                            Price = 1000.00m,
                            Availability = "200",
                            CategoryId = 3 // Sports
                        },
                        new Event
                        {
                            EventId = 4,
                            EventName = "Rock Revolution 2026",
                            EventDate = DateTime.Now.AddDays(15),
                            Venue = "National Stadium",
                            Price = 1800.00m,
                            Availability = "100", 
                            CategoryId = 1 // Music
                        }
                    );

                    try
                    {
                        context.SaveChanges();
                        Console.WriteLine("Database Seeded Successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error seeding database: " + ex.Message);
                    }
                }
            }
        }
    }
}