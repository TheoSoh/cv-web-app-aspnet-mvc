using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestDbContext _context;

        public HomeController(ILogger<HomeController> logger, TestDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            // Hämta de senaste projekten, t.ex. de 5 senaste
            var latestProjects = _context.Projects
                .OrderByDescending(p => p.CreatedDate) // Antag att du har en kolumn "CreatedAt"
                .Take(5) // Hämta de senaste 5 projekten, ändra antalet efter behov
                .ToList();

            return View(latestProjects);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
