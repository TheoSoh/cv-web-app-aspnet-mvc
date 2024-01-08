using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            HomePageViewModel model = new HomePageViewModel { };
            
            //Hämtar 3 CVn från databasen
            model.Users = _context.Users.Where(u => !u.PrivateStatus).Where(u => u.Cv != null).Where(u => !u.IsDeactivated).Take(3).ToList();

            //Hämtar det senaste projektet och sorterar genom datum de skapades (fallande) samt konverterar resultatet till lista
            model.Projects = _context.Projects
                .OrderByDescending(p => p.CreatedDate) 
                .Take(1) 
                .ToList();

            return View(model);
        }
    }
}
