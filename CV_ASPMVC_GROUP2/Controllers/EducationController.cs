using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class EducationController : BaseController
    {
        private TestDbContext context;
       
        public EducationController(TestDbContext context) 
        { 
                this.context = context;
                
        }
       

        public IActionResult Index()
        {
            var items = context.Educations.ToList();
            return View(items);
        }


        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducation(Education evm)
        {


            if (ModelState.IsValid)
            {

                var education = new Education();

                education.Name = evm.Name;
                education.Description = evm.Description;
                await context.AddAsync(education);
                await context.SaveChangesAsync();

                string currentUserId = base.UserId;
                int currentCvId = context.Cvs.Where(c => c.User_ID == currentUserId).Single().Id;

                var cvEducation = new CvEducation();
                cvEducation.CvId = currentCvId;
                cvEducation.Education = education;
                await context.AddAsync(cvEducation);
                await context.SaveChangesAsync();

                return RedirectToAction("CreateEducation", "Education");

            }
            return View(evm);

        }

    }
}
