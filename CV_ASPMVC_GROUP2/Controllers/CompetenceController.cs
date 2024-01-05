using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class CompetenceController : BaseController
    {
        private TestDbContext context;

        public CompetenceController(TestDbContext context) 
        {
            this.context = context;
            
        }
        

        public IActionResult Index()
        {
            var items = context.Competences.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult CreateCompetence()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompetence(Competence cvm)
        {


            if (ModelState.IsValid)
            {

                var competence = new Competence();

                competence.Name = cvm.Name;
                competence.Description = cvm.Description;
                await context.AddAsync(competence);
                await context.SaveChangesAsync();

                string currentUserId = base.UserId;
                int currentCvId = context.Cvs.Where(c => c.User_ID == currentUserId).Single().Id;

                var cvCompetence = new CvCompetence();
                cvCompetence.Competence = competence;
                cvCompetence.CvId = currentCvId;
                await context.AddAsync(cvCompetence);
                await context.SaveChangesAsync();

                return RedirectToAction("CreateCompetence", "Competence");

            }
            return View(cvm);

        }

    }
}
