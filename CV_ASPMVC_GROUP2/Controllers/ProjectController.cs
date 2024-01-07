using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using Microsoft.CodeAnalysis;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class ProjectController : BaseController
    {
        private TestDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectController(TestDbContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnvironment = webHostEnviroment;
        }
        public IActionResult Index()
        {
            //Hämtar alla projekt från databasen och lagrar dem i en lista och visar dem i vyn
            var items = _context.Projects.ToList();
            ViewBag.CurrentUserId = base.UserId;
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel pm)
        {
            if(ModelState.IsValid) {

                string stringFile = UploadFile(pm);

                //Skapar en ny instans av Project och tilldelar attributen
                var project = new Models.Project();

                project.Name = pm.Title;
                project.Description = pm.Description;
                project.Image = stringFile;
                project.CreatedByUserId = base.UserId;

                //Lägger till det nya projektet i databasen och sparar
                await _context.AddAsync(project);
                await _context.SaveChangesAsync();

                //Skapar en koppling mellan projekt och användare
                var userProject = new UserProject();
                userProject.UserId = base.UserId;
                userProject.Project = project;
                await _context.AddAsync(userProject);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Project");

            }
            return View(pm);
           
        }

        [HttpGet]
        public IActionResult EditProject(int? id)
        {
            //Hämtar projektet vi vill ändra/editera
            var pro = _context.Projects.FirstOrDefault(x => x.Id == id);
           
            var model = new EditProjectViewModel
            {
                Title = pro.Name,
                Description = pro.Description,
                ImageFile = pro.ImageFile
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditProject(EditProjectViewModel pm, int id)
        {
            try
            {
                string stringFile = UploadFile(pm);

                //Hämtar projekt baserat på ID
                var pro = _context.Projects.FirstOrDefault(x => x.Id == id);

                //Tilldelar attribut
                pro.Name = pm.Title;
                pro.Description = pm.Description;
                if (stringFile != null)
                {
                    pro.Image = stringFile;
                }

                //Uppdaterar projektet i databasen och sparar ändringarna
                _context.Update(pro);
                _context.SaveChanges();


                //Omdirigerar användaren till index sidan för projekt
                return RedirectToAction("Index", "Project");
            }
            catch (Exception ex)
            {

                return View(pm);
            }
        }
    

        private string UploadFile(ProjectViewModel pm)
        {
            string fileName = null;

            //Kontrollerar om det finns en fil att ladda upp
            if (pm.ImageFile != null)
            {
                //Skapar sökväg för uppladdning av filen till mappen "Images" i wwwroot
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                //Kontrollerar om mappen inte finns och skapar den 
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                fileName = Guid.NewGuid().ToString() + "-" + pm.ImageFile.FileName;

                //Skapar ett unikt filnamn för den uppladdade filen
                string filePath = Path.Combine(uploadDir, fileName);

                //Sparar filen på servern
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pm.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }



        private string UploadFile(EditProjectViewModel pm)
        {
            string fileName = null;

            //Kontrollerar om det finns en fil att ladda upp
            if (pm.ImageFile != null)
            {
                //Skapar sökväg för uppladdning av filen till mappen "Images" i wwwroot
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                //Kontrollerar om mappen inte finns och skapar den 
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                fileName = Guid.NewGuid().ToString() + "-" + pm.ImageFile.FileName;

                //Skapar ett unikt filnamn för den uppladdade filen
                string filePath = Path.Combine(uploadDir, fileName);

                //Sparar filen på servern
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pm.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            //Hämtar projektet som ska raderas
            Models.Project project = _context.Projects.Find(id);
            return View(project);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Models.Project project) 
        { 
            //Raderar projektet från databasen och sparar detta
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("Index", "Project");
        
        }

        public IActionResult Join(int id)
        {
            //Hämtar det aktuella ID:t för projekt
            Models.Project project = _context.Projects.Find(id);
            return View(project);
        }

        [HttpPost]

        //public async Task<IActionResult> Join(int? id)
        //{

        //    if (id == null || _context.Projects == null)
        //    {
        //        return NotFound();

        //    }
        //    var joina = await _context.Projects.FindAsync(id);
        //    if (joina == null)
        //    {
        //        return NotFound();
        //    }

        //    var anv = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
        //    var pro = _context.Projects.FirstOrDefault(x => x.Id == id);

        //    UserProject userProject = new UserProject();
        //    userProject.UserId = anv.Id;
        //    userProject.ProjectId = pro.Id;
        //    _context.Add(userProject);


        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Index", "Project");
        //}

        public async Task<IActionResult> Join(int? id)
        {
            try
            {
                if (id == null || _context.Projects == null)
                {
                    return NotFound();
                }

                //Hämtar projekt-ID och kontrollerar om det är null
                var project = await _context.Projects.FindAsync(id);
                if (project == null)
                {
                    return NotFound();
                }

                //Hämtar den första användaren från databasen där användarnamnet matchar det inloggade användarnamnet
                var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

                // Kontrollera om användaren redan deltar i projektet
                var existingUserProject = _context.Set<UserProject>()
                    .FirstOrDefault(up => up.UserId == user.Id && up.ProjectId == project.Id);

                if (existingUserProject != null)
                {
                    // Användaren deltar redan i projektet, visa ett meddelande
                    TempData["Message"] = "Du deltar redan i detta projekt.";
                    return RedirectToAction("Join", "Project");
                }
                else
                {
                    // Användaren deltar inte i projektet, lägg till UserProject
                    UserProject userProject = new UserProject
                    {
                        UserId = user.Id,
                        ProjectId = project.Id
                    };
                    _context.Add(userProject);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Du har gått med i projektet!";
                }

                return RedirectToAction("Join", "Project");
            }
            catch (Exception ex)
            {
                //Hanterar undantag
                TempData["ErrorMessage"] = "Ett fel inträffade vid försök att gå med i projektet.";
                return RedirectToAction("Join", "Project");
            }
        }
    }
}

