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
            var selectedCVs = _context.Cvs.Include(cv => cv.User).Take(5).ToList();
            var latestProject = _context.Projects.OrderByDescending(p => p.CreatedDate).FirstOrDefault();

            ViewBag.SelectedCVs = selectedCVs;
            ViewBag.LatestProject = latestProject;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
