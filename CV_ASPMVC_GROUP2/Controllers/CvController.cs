using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;



namespace CV_ASPMVC_GROUP2.Controllers
{
    public class CvController : BaseController
    {
        private TestDbContext context;
        public CvController(TestDbContext context)
        {
            this.context = context;
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


        //public IActionResult CvList()
        //{
        //    return View();
        //}

        //public IActionResult Delete(Cv cv)
        //{
        //    return RedirectToAction();
        //}
    }
}
