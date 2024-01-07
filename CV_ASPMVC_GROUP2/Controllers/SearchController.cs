using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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

            //Kontrollerar om söksträngen är tom
            if (!string.IsNullOrEmpty(searchString))
            {
                //Hämtar användare där antingen förnamn eller användarnamn innehåller söksträngen
                users = users.Where(u => u.UserName.Contains(searchString) || u.FirstName.Contains(searchString));

                //Kontrollerar om användaren är inloggad
                if (User.Identity.IsAuthenticated)
                {
                    users = users.Include(u => u.Cv); 
                }
                else
                {
                    users = users.Where(u => u.PrivateStatus == false).Include(u => u.Cv);
                }
            }

            //Lagrar användare i en lista och returnerar vyn för dem
            var searchResult = users.ToList();
            return View(searchResult);
        }
    }
}


