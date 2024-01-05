using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            IQueryable<User> users = _context.Users;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString) || u.FirstName.Contains(searchString));

                if (User.Identity.IsAuthenticated)
                {
                    users = users.Include(u => u.Cv); 
                }
                else
                {
                    users = users.Where(u => u.PrivateStatus);
                }
            }

            var searchResult = users.ToList();
            return View(searchResult);
        }

    }
}


