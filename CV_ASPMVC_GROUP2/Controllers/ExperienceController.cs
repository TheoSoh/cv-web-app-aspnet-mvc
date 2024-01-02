using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var model = new ExperienceViewModel();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ExperienceViewModel experienceviewmodel)
        {
            if (ModelState.IsValid)
            {

                var experience = new Experience();

                experience.Name = experienceviewmodel.Name;
                experience.Description = experienceviewmodel.Description;
                await _context.AddAsync(experience);
                await _context.SaveChangesAsync();


                var cvExperience = new CvExperience();
                //cvExperience.UserId = base.UserId;
                cvExperience.Experience = experience;
                await _context.AddAsync(cvExperience);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "Cv");

            }
            return View(experienceviewmodel);

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
