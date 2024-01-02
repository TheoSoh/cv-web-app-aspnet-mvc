﻿using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class ProfileController : BaseController
    {
        private TestDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(TestDbContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnvironment = webHostEnviroment;
        }



        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]

        public IActionResult UploadFile()
        { 
        
            return View();
        
        }

        [HttpPost]
        public IActionResult UploadFile(ProfileViewModel profile)
        {
           
            if (profile.ImageFile != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string fileName = Guid.NewGuid().ToString() + "-" + profile.ImageFile.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profile.ImageFile.CopyTo(fileStream);
                }

                User anv = _context.Users.Where(c => c.UserName.Equals(User.Identity.Name)).FirstOrDefault();
                String PhotoPath = fileName;
                anv.ProfilePicture = fileName;
                _context.Update(anv);
                _context.SaveChanges();


            }

            return RedirectToAction("Index", "Profile");


        }
    }
}