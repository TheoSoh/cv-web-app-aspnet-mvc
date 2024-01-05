using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;

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
                project.CreatedByUserId = base.UserId;
                await _context.AddAsync(project);
                await _context.SaveChangesAsync();

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
            var pro = _context.Projects.FirstOrDefault(x => x.Id == id);

            var model = new ProjectViewModel
            {
                Title = pro.Name,
                Description = pro.Description,
                ImageFile = pro.ImageFile


            };

            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> EditProject(ProjectViewModel pm, int id)
        {
            try
            {

                string stringFile = UploadFile(pm);
                var pro = _context.Projects.FirstOrDefault(x => x.Id == id);

                pro.Name = pm.Title;
                pro.Description = pm.Description;
                pro.Image = stringFile;



                _context.Update(pro);
                _context.SaveChanges();



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

        [HttpGet]
        

        public IActionResult Delete(int id)
        {
            Project project = _context.Projects.Find(id);
            return View(project);
        }


        [HttpPost]

        public async Task<IActionResult> Delete(Project project) 
        { 
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("Index", "Project");
        
        }

        public IActionResult Join(int id)
        {
            Project project = _context.Projects.Find(id);
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int? id)
        {

            if (id == null || _context.Projects == null)
            {
                return NotFound();

            }
            var joina = await _context.Projects.FindAsync(id);
            if (joina == null)
            {
                return NotFound();
            }

            var anv = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var pro = _context.Projects.FirstOrDefault(x => x.Id == id);
           
                UserProject userProject = new UserProject();
                userProject.UserId = anv.Id;
                userProject.ProjectId = pro.Id;
                _context.Add(userProject);


            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Project");
        }


    }
}


        //public IActionResult ProjectList()
        //{
        //    return View();
        //}

        //public IActionResult Delete(Project project)
        //{
        //    return RedirectToAction(nameof(ProjectList));
        //}
 
