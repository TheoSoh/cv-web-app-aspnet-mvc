using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class CvController : BaseController
    {
        private TestDbContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CvController(TestDbContext context, IWebHostEnvironment _webHostEnvironment)
        {
            this.context = context;
            this._webHostEnvironment = _webHostEnvironment;

        }

        //Här är en action metod som skapar vyn och skickar den lämpliga vyn "showcvview" till användaren. Här visas cv:et för den användare som man klickar in på via profilsidan.  
        [HttpGet]
        public IActionResult ShowCv(string userId)
        {
            ShowCvViewModel viewModel = new ShowCvViewModel { };
            viewModel.User = context.Users.Where(u => u.Id == userId).Single();
            viewModel.Cv = context.Cvs.Where(c => c.User_ID == userId).Single();

            IEnumerable<CvCompetence> cvCompetences = context.CvCompetences.Where(cc => cc.CvId == viewModel.Cv.Id).ToList();
            List<Competence> competencesToView = new List<Competence>();
            foreach (var cvCompetence in cvCompetences)
            {
                competencesToView.Add(context.Competences.Where(c => c.Id == cvCompetence.CompetenceId).Single());
            }
            viewModel.Competences = competencesToView;

            IEnumerable<CvExperience> cvExperiences = context.CvExperiences.Where(cc => cc.CvId == viewModel.Cv.Id).ToList();
            List<Experience> experiencesToView = new List<Experience>();
            foreach (var experience in cvExperiences)
            {
                experiencesToView.Add(context.Experiences.Where(ex => ex.Id == experience.ExperienceId).Single());
            }
            viewModel.Experiences = experiencesToView;

            IEnumerable<CvEducation> cvEducations = context.CvEducations.Where(cc => cc.CvId == viewModel.Cv.Id).ToList();
            List<Education> educationsToView = new List<Education>();
            foreach (var education in cvEducations)
            {
                educationsToView.Add(context.Educations.Where(ed => ed.Id == education.EducationId).Single());
            }
            viewModel.Educations = educationsToView;
            return View(viewModel);
        }



        //Metoden här gör så att vyn kommer att göras synlig på hemsidan. den tar emot en request och skickar den lämpliga vyn till användaren  
        [HttpGet]
        public IActionResult CreateCv()
        {
            return View();
        }

        //Metoden begär en resurs tex i detta fall så vill vi skapa ett objekt i databasen
        [HttpPost]
        public async Task<IActionResult> CreateCv(Cv cvm)
        {
            if (ModelState.IsValid)
            {

                string stringFile = UploadFile(cvm);
                var cv = new Cv();


                cv.Description = cvm.Description;
                cv.CvImage = stringFile;
                cv.User_ID = base.UserId;
                await context.AddAsync(cv);
                await context.SaveChangesAsync();

                return RedirectToAction("CreateCv", "Cv");

            }
            return View(cvm);

        }

        //Metod som utför en filuppladdningsfunktion för ett cv. _WebhostEnvironment.WebRootPath slapar en sökväg där filen/bilden kommer att sparas
        //Den kollar sedan om sökvägen "Uploaddir" inte finns om inte så skapas den. 
        //Sedan genereras ett unikt filnamn
        //FileStream = skapar filen på den anvgivna filvägen 
        //till sist returnerar det genererade filnamnet. 
        //koden hanterar uppladdningen av en fil för en bild till ett cv och sedan sparar den på servern under katalogen för Images
        private string UploadFile(Cv cvm)
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

        public IActionResult CvList()
        {
            return View();
        }




        [HttpGet]
        public IActionResult EditCv(int? id)
        {
            var cro = context.Cvs.FirstOrDefault(x => x.Id == id);


            var model = new EditCvViewModel()
            {
                Description = cro.Description,
                ImageFile = cro.ImageFile

            };
            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> EditCv(EditCvViewModel pm, int id)
        {
            try
            {

                string stringFile = UploadFile(pm);
                var pro = context.Cvs.FirstOrDefault(x => x.Id == id);


                pro.Description = pm.Description;
                pro.CvImage = stringFile;



                context.Update(pro);
                context.SaveChanges();



                return RedirectToAction("ShowCv", "Cv");
            }
            catch (Exception ex)
            {

                return View(pm);
            }
        }

        private string UploadFile(EditCvViewModel pm)
        {
            string fileName = null;
            if (pm.ImageFile != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                fileName = Guid.NewGuid().ToString() + "-" + pm.ImageFile.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pm.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }

    }

}

       