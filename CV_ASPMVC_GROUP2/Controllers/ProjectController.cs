using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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
            var items = _context.Projects.ToList();
            return View(items);
        }

        [HttpGet]
        //public IActionResult Create()
        //{
        //    //var items = _context.Projects.ToList();
        //    //return View(items);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel pm)
        {
            if(ModelState.IsValid) {

                string stringFile = UploadFile(pm);
                var project = new Project();

                project.Name = pm.Title;
                project.Description = pm.Description;
                project.Image = stringFile;
                await _context.AddAsync(project);
                await _context.SaveChangesAsync();

                //var userProject = new UserProject();
                //userProject.UserId = 

                var userProject = new UserProject();
                userProject.UserId = base.UserId;
                userProject.Project = project;
                await _context.AddAsync(userProject);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Project");

            }
            return View(pm);
           
        }

        private string UploadFile(ProjectViewModel pm)
        {
            string fileName = null;
            if(pm.Image!= null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + pm.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pm.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public IActionResult ProjectList()
        {
            return View();
        }

        public IActionResult Delete(Project project)
        {
            return RedirectToAction(nameof(ProjectList));
        }
    }
}
