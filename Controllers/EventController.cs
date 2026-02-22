using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Event_Management_and_Ticketing_System.Models;
using Smart_Event_Management_and_Ticketing_System.Data;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Smart_Event_Management_and_Ticketing_System.Controllers
{
    public class EventController : Controller
    {
        private readonly EventSystemContext _context;

        public EventController(EventSystemContext context)
        {
            _context = context;
        }

        //  BROWSE & SEARCH EVENTS 
        public async Task<IActionResult> Index(string searchString)
        {
            var events = _context.Events.Include(e => e.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                string searchLower = searchString.ToLower();
                decimal searchPrice;
                bool isNumeric = decimal.TryParse(searchString, out searchPrice);

                DateTime searchDate;
                bool isDate = DateTime.TryParse(searchString, out searchDate);

                events = events.Where(s => s.EventName.ToLower().Contains(searchLower)
                                        || s.Venue.ToLower().Contains(searchLower)
                                        || (s.Category != null && s.Category.CategoryType.ToLower().Contains(searchLower))
                                        || (isNumeric && s.Price == searchPrice)
                                        || (isDate && s.EventDate.HasValue && s.EventDate.Value.Date == searchDate.Date));
            }

            return View(await events.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.Events
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (eventItem == null) return NotFound();

            var userId = HttpContext.Session.GetString("UserId");
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(userId);

            ViewBag.Reviews = await _context.Reviews
                .Include(r => r.Member)
                .Where(r => r.EventId == id)
                .ToListAsync();

            ViewBag.SeatTypes = await _context.SeatTypes.ToListAsync();

            return View(eventItem);
        }

        //  BOOKING LOGIC WITH INVENTORY VALIDATION 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookTickets(decimal EventId, decimal ticketTypeId, int Quantity)
        {
            //  Check the Session manually
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "/Event/Details/" + EventId });
            }

            //  Logic to handle the Event and Availability
            var eventItem = await _context.Events.FindAsync(EventId);
            if (eventItem == null) return NotFound();

            //  REVISED VALIDATION LOGIC FOR TEST CASE 
            if (decimal.TryParse(eventItem.Availability, out decimal currentAvail))
            {
                // Trigger Error if requested quantity exceeds stock
                if (Quantity > currentAvail)
                {
                    // This creates the error message for  report
                    TempData["Error"] = $"Booking Rejected: Only {currentAvail} seats are available.";

                    // Stay on the details page so the user sees the error
                    return RedirectToAction("Details", new { id = EventId });
                }

                // If check passes, perform the subtraction
                eventItem.Availability = (currentAvail - Quantity).ToString();
                _context.Update(eventItem);
            }

            // Create the Booking record
            decimal nextId = (_context.Bookings.Any() ? _context.Bookings.Max(b => b.BookingId) : 0) + 1;

            var newBooking = new Booking
            {
                BookingId = nextId,
                EventId = EventId,
                MemberId = decimal.Parse(userIdStr),
                SeatTypeId = ticketTypeId,
                Quantity = (decimal)Quantity
            };

            _context.Bookings.Add(newBooking);

            // Save changes
            await _context.SaveChangesAsync();

            return RedirectToAction("PaymentPanel", "Account", new { bookingId = nextId });
        }

        //  REVIEW METHODS 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(decimal EventId, decimal Rating, string Comment)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "/Event/Details/" + EventId });
            }

            decimal currentMemberId = decimal.Parse(userIdStr);
            decimal nextId = (_context.Reviews.Any() ? _context.Reviews.Max(r => r.ReviewId) : 0) + 1;

            var newReview = new Review
            {
                ReviewId = nextId,
                EventId = EventId,
                Rating = Rating,
                Comments = Comment,
                MemberId = currentMemberId
            };

            _context.Reviews.Add(newReview);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = EventId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(decimal id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (userIdStr != review.MemberId.ToString()) return Forbid();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Review deleted successfully.";
            return RedirectToAction("Details", new { id = review.EventId });
        }

        [HttpGet]
        public async Task<IActionResult> EditReview(decimal id)
        {
            var review = await _context.Reviews.FindAsync(id);
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (review == null || userIdStr == null || review.MemberId.ToString() != userIdStr)
            {
                return Forbid();
            }

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(Review model)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            var review = await _context.Reviews.FindAsync(model.ReviewId);

            if (review == null || userIdStr == null || review.MemberId.ToString() != userIdStr)
            {
                return Forbid();
            }

            review.Rating = model.Rating;
            review.Comments = model.Comments;

            _context.Update(review);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Review updated successfully!";
            return RedirectToAction("Details", new { id = review.EventId });
        }

        //  OTHER ACTIONS 
        public async Task<IActionResult> MyBookings()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (userIdStr == null) return RedirectToAction("Login", "Account");

            decimal currentMemberId = decimal.Parse(userIdStr);
            var myBookings = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.SeatType)
                .Where(b => b.MemberId == currentMemberId)
                .ToListAsync();

            return View(myBookings);
        }

        public IActionResult Inquiry() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendInquiry(string name, string email, string message)
        {
            decimal nextId = (_context.Inquiries.Any() ? _context.Inquiries.Max(i => i.InquiryId) : 0) + 1;
            var userIdStr = HttpContext.Session.GetString("UserId");
            decimal? currentMemberId = userIdStr != null ? decimal.Parse(userIdStr) : null;

            var newInquiry = new Inquiry
            {
                InquiryId = nextId,
                Fullname = name,
                Email = email,
                Message = message,
                MemberType = userIdStr != null ? "Member" : "Guest",
                MemberId = currentMemberId
            };

            _context.Inquiries.Add(newInquiry);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thank you! Your inquiry has been saved.";
            return RedirectToAction("Inquiry");
        }

        public async Task<IActionResult> Receipt(decimal id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.SeatType)
                .Include(b => b.Payment)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null) return NotFound();

            var userIdStr = HttpContext.Session.GetString("UserId");
            if (userIdStr == null || booking.MemberId != decimal.Parse(userIdStr)) return Forbid();

            return View(booking);
        }
    }
}