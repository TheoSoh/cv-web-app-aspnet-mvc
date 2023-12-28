using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;
using CV_ASPMVC_GROUP2.Repositories.Abstract;



namespace CV_ASPMVC_GROUP2.Controllers
{
    public class CvController : Controller
    {
        private TestDbContext context;
        private readonly ICvService icvService;
        private ICvService<Cv> CvData { get; set; }
        public CvController(TestDbContext context, ICvService icvService)
        {
            this.context = context;
            this.icvService = icvService;
        }

        [HttpPost]
        public IActionResult AddCV()
        {
            
            //context.Cvs.Add(cv);
            //context.SaveChanges();
            //return RedirectToAction("AddCV", "Home");
            return View("AddCV");
        }

        [HttpGet]
        public IActionResult CVs()
        {
            var items = context.Cvs.ToList();
            return View(items);
            //List<Cv> list = CvData.GetAll().ToList();
            //return View(list);
        }


        public IActionResult CvList()
        {
            var data = this.icvService.GetAll().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = icvService.Delete(id);
            return RedirectToAction(nameof(CvList));
        }
    }
}
