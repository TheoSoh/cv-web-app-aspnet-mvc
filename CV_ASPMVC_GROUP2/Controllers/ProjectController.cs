using CV_ASPMVC_GROUP2.Models;
using CV_ASPMVC_GROUP2.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class ProjectController : Controller
    {
        private TestDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProjectService iProjectService;

        public ProjectController(TestDbContext context, IWebHostEnvironment webHostEnviroment, IProjectService iProjectService)
        {
            _context = context;
            _webHostEnvironment = webHostEnviroment;
            this.iProjectService = iProjectService;
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
                var Project = new Project();

                Project.Name = pm.Title;
                Project.Description = pm.Description;
                Project.Image = stringFile;
                await _context.AddAsync(Project);
                await _context.SaveChangesAsync();
                
                //var userProject = new UserProject();
                //userProject.UserId = 



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
            var data = this.iProjectService.GetAll().ToList();
            return View(data);

        }

        public IActionResult Delete(int id)
        {
            var result = iProjectService.Delete(id);
            return RedirectToAction(nameof(ProjectList));
        }
    }
}
