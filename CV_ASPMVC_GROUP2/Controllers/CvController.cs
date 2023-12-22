using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class CvController : Controller
    {
        private TestDbContext context;
        public CvController(TestDbContext context)
        {
            this.context = context;
        }
        
        public IActionResult AddCV()
        {
            return View("AddCV");
        }

        public IActionResult CVs()
        {
            var items = context.Cvs.ToList();
            return View(items);
        }
    }
}
