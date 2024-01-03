using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class SearchController : Controller
    {
        TestDbContext _context;

        public SearchController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var model = _context.Users.Where(u => u.UserName.Contains(searchString) || u.FirstName.Contains(searchString)).ToList();

            return View(model);
        }
    }
}


