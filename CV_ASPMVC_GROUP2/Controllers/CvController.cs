using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;



namespace CV_ASPMVC_GROUP2.Controllers
{
    public class CvController : BaseController
    {
        private TestDbContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CvController(TestDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            _webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Index()
        {
            var items = context.Cvs.ToList();
            return View(items);
        }


        [HttpPost]

        public async Task<IActionResult> AddCV(CvViewModel cvViewModel)
        {

            if (ModelState.IsValid)
            {
                var cv = new Cv { Id = cvViewModel.Id, Description = cvViewModel.Description };
                context.Cvs.Add(cv);
                context.SaveChangesAsync();

                if (cvViewModel.SelectedEducations != null)
                {
                    var educations = context.Educations.Where(e => cvViewModel.SelectedEducations.Contains(e.Id)).ToList();
                    cv.CvEducations = educations.Select(e => new CvEducation { CvId = cv.Id, EducationId = e.Id}).ToList();
                }

                // Här ska en erfarenhet läggas till för CV som man får välja från en flervalslista
                if (cvViewModel.SelectedExperience != null)
                {
                    var experiences = context.Experiences.Where(ex => cvViewModel.SelectedExperience.Contains(ex.Id)).ToList();
                    cv.CvExperiences = experiences.Select(ex => new CvExperience { CvId = cv.Id, ExperienceId = ex.Id }).ToList();
                }

                // samma som ovan fast för kompetens för cv
                if (cvViewModel.SelectedCompetence != null)
                {
                    var competences = context.Competences.Where(c => cvViewModel.SelectedCompetence.Contains(c.Id)).ToList();
                    cv.CvCompetences = competences.Select(c => new CvCompetence { CvId = cv.Id, CompetenceId = c.Id }).ToList();
                }


                context.Cvs.Add(cv);
                await context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }


            return View(cvViewModel);
        }



        [HttpGet]
        public IActionResult AddCV()
        {

            var model = new CvViewModel
            {
                AvailableEducations = context.Educations.Select(e => new EducationViewModel { Id = e.Id, Name = e.Name, Description = e.Description }).ToList(),

                AvailableExperience = context.Experiences.Select(ex => new ExperienceViewModel { Id = ex.Id, Name = ex.Name, Description = ex.Description }).ToList(),

                AvailableCompetence = context.Competences.Select(c => new CompetenceViewModel { Id = c.Id, Name = c.Name, Description = c.Description  }).ToList()

            };
            

            // Här läggs exempeldata till valde bara några - så om listan är tom så läggs exempeldatan till
            if (model.AvailableExperience.Count == 0)
            {
                model.AvailableExperience.Add(new ExperienceViewModel { Id = 1, Name = "Kundtjänstmedarbetare" });
                
            }

            if (model.AvailableEducations.Count == 0)
            {
                model.AvailableEducations.Add(new EducationViewModel { Id = 1, Name = "Systemvetare" });
                
            }

            if (model.AvailableCompetence.Count == 0)
            {
                model.AvailableCompetence.Add(new CompetenceViewModel { Id = 1, Name = "JavaScript" });
                
            }

            return View(model);

        }

        
        

        public IActionResult CvList()
        {
            return View();
        }

        //public IActionResult Delete(Cv cv)
        //{
        //    return RedirectToAction();
        //}


        private string UploadFile(CvViewModel cvm)
        {
            string fileName = null;
            if (cvm.ImageFile != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                fileName = Guid.NewGuid().ToString() + "-" + cvm.ImageFile.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    cvm.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }


        public IActionResult Indexx()
        {
            var items = context.Experiences.ToList();
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
                await context.AddAsync(experience);
                await context.SaveChangesAsync();


                var cvExperience = new CvExperience();
                //cvExperience.UserId = base.UserId;
                cvExperience.Experience = experience;
                await context.AddAsync(cvExperience);
                await context.SaveChangesAsync();

                return RedirectToAction("Indexx", "Cv");

            }
            return View(experienceviewmodel);

        }



        public IActionResult Indexxx()
        {
            var items = context.Educations.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult CreateEducation()
        {
            var model = new EducationViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducation(EducationViewModel educationviewmodel)
        {


            if (ModelState.IsValid)
            {

                var education = new Education();

                education.Name = educationviewmodel.Name;
                education.Description = educationviewmodel.Description;
                await context.AddAsync(education);
                await context.SaveChangesAsync();


                var cvEducation = new CvEducation();
                cvEducation.Education = education;
                await context.AddAsync(cvEducation);
                await context.SaveChangesAsync();

                return RedirectToAction("CreateEducation", "Cv");

            }
            return View(educationviewmodel);

        }

        public IActionResult Indexxxx()
        {
            var items = context.Competences.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult CreateCompetence()
        {
            var model = new CompetenceViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompetence(CompetenceViewModel competenceviewmodel)
        {


            if (ModelState.IsValid)
            {

                var competence = new Competence();

                competence.Name = competenceviewmodel.Name;
                competence.Description = competenceviewmodel.Description;
                await context.AddAsync(competence);
                await context.SaveChangesAsync();


                var cvCompetence = new CvCompetence();
                cvCompetence.Competence = competence;
                await context.AddAsync(cvCompetence);
                await context.SaveChangesAsync();

                return RedirectToAction("CreateCompetence", "Cv");

            }
            return View(competenceviewmodel);

        }



    }
}
