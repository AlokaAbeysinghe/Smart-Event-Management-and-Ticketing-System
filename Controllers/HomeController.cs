using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Event_Management_and_Ticketing_System.Data;
using Smart_Event_Management_and_Ticketing_System.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Smart_Event_Management_and_Ticketing_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventSystemContext _context;

        public HomeController(EventSystemContext context)
        {
            _context = context;
        }

        //  PUBLIC HOME PAGE 
        // Guests and Members can both access this to browse events 
        public async Task<IActionResult> Index(string searchString)
        {
            // Security Gate removed here so the page is public 
            var eventsQuery = _context.Events.Include(e => e.Category).AsQueryable();

            // Search functionality 
            if (!string.IsNullOrEmpty(searchString))
            {
                string searchLower = searchString.ToLower();

                decimal searchPrice;
                bool isNumeric = decimal.TryParse(searchString, out searchPrice);

                DateTime searchDate;
                bool isDate = DateTime.TryParse(searchString, out searchDate);

                eventsQuery = eventsQuery.Where(s => s.EventName.ToLower().Contains(searchLower)
                                        || s.Venue.ToLower().Contains(searchLower)
                                        || (s.Category != null && s.Category.CategoryType.ToLower().Contains(searchLower))
                                        || (isNumeric && s.Price == searchPrice)
                                        || (isDate && s.EventDate.HasValue && s.EventDate.Value.Date == searchDate.Date));
            }

            return View(await eventsQuery.ToListAsync());
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}