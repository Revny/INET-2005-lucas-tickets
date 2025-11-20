
using eventlist.Models;
using lucas_tickets.Data;
using lucas_tickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace lucas_tickets.Controllers
{
    [Authorize]
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
        private readonly lucas_ticketsContext _context;

        public HomeController(ILogger<HomeController> logger, lucas_ticketsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var show = await _context.Shows
               .OrderByDescending(m => m.Createdate)
               .Include(p => p.Category)
               .ToListAsync();

            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // GET: Shows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shows = await _context.Shows
                .Include(s => s.Category)
                .Include(s => s.Payment)
                .FirstOrDefaultAsync(m => m.ShowId == id);
            if (shows == null)
            {
                return NotFound();
            }

            return View(shows);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    }




       
        