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
        public IActionResult Index(string search, string id)
        {


            var model = _context.Users.Where(u => u.UserName.StartsWith(search) || u.FirstName.StartsWith(search)).ToList();


            return View(model);
        }



      


    }


    }


