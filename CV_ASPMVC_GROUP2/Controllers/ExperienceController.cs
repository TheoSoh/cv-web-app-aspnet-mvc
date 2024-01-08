using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [Authorize]
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

                return RedirectToAction("ShowCv", "Cv");

            }
            return View(ex);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            Models.Experience experience = _context.Experiences.Find(id);
            //Tar bort erfarentets-sobjektet från databasen och sparar ändringarna i databasen
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowCv", "Cv");

        }
    }
}
