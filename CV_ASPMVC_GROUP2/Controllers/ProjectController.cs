using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class ProjectController : Controller
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

        public IActionResult Create()
        {
            var items = _context.Projects.ToList();
            return View(items);
        }

        [HttpPost]
        public IActionResult Create(ProjectViewModel pm)
        {
            string stringFile = UploadFile(pm);
            var Project = new Project
            {
                Name = pm.Title,
                Description = pm.Description,
                Image = stringFile
            };
            _context.Projects.Add(Project);
            _context.SaveChanges();

            //var Project_user = new UserProject
            //{
            //    UserId = 
            //}
            
            

            return RedirectToAction("Index","Project");
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
    }
}
