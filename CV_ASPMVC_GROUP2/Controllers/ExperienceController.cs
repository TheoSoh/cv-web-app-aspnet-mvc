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
                //Skapar en ny instans av experience och tilldelar attribut
                var experience = new Experience();

                experience.Name = ex.Name;
                experience.Description = ex.Description;

                //Lägger till experience i databasen och sparar ändringarna
                await _context.AddAsync(experience);
                await _context.SaveChangesAsync();

                //Hämtar användarens nuvarande CV ID baserat på användar-ID
                string currentUserId = base.UserId;
                int currentCvId = _context.Cvs.Where(c => c.User_ID == currentUserId).Single().Id;

                //Skapar en koppling mellan CV och den skapade erfarenheten
                var cvExperience = new CvExperience();
                cvExperience.CvId = currentCvId;
                cvExperience.Experience = experience;

                //Lägger till kopplingen i databasen
                await _context.AddAsync(cvExperience);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "Experience");

            }
            return View(ex);

        }

        [HttpGet]
        public IActionResult DeleteExperience(int id)
        {
            //Hämtar experience-objektet som matchar det angivna ID:t
            Models.Experience experience = _context.Experiences.Find(id);
            return View(experience);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExperience(Models.Experience experience)
        {
            //Tar bort erfarentets-sobjektet från databasen och sparar ändringarna i databasen
            _context.Experiences.Remove(experience);
            _context.SaveChanges();
            return RedirectToAction("DeleteExperience", "Experience");

        }

        [HttpGet]
        public IActionResult Indexx()
        {
            return View();
        }

        public IActionResult ExperienceList()
        {
            return View();
        }
    }
}
