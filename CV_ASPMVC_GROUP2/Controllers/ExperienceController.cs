using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class ExperienceController : BaseController
    {
        private TestDbContext _context;
        public ExperienceController(TestDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.Experiences.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Experience ex)
        {
            if (ModelState.IsValid)
            {

                var experience = new Experience();

                experience.Name = ex.Name;
                experience.Description = ex.Description;
                await _context.AddAsync(experience);
                await _context.SaveChangesAsync();

                string currentUserId = base.UserId;
                int currentCvId = _context.Cvs.Where(c => c.User_ID == currentUserId).Single().Id;

                var cvExperience = new CvExperience();
                cvExperience.CvId = currentCvId;
                cvExperience.Experience = experience;
                await _context.AddAsync(cvExperience);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "Experience");

            }
            return View(ex);

        }

   
        //public IActionResult ProjectList()
        //{
        //    return View();
        //}



        public IActionResult ExperienceList()
        {
            return View();
        }
    }
}
