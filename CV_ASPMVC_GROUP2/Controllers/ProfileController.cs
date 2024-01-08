using Microsoft.AspNetCore;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Collections;

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

        [HttpGet]
        public IActionResult Index(string userId)
        {
            //Hämtar den aktuella inloggade användarens ID
            string currentUserId = base.UserId;


            User user = null;
            Address address = null;
            Cv cv = null;
            //Om användar-ID inte är specificerat används den inloggade användarens ID för att hämta dess information
            if (userId == null)
            {
                user = _context.Users.Where(u => u.Id.Equals(currentUserId)).Single();
                address = _context.Addresses.Where(a => a.UserId == currentUserId).Single();
                try
                {
                    //Försöker hämta CV-informationen för den inloggade användaren
                    cv = _context.Cvs.Where(c => c.User_ID == currentUserId).Single();
                }
                catch (Exception ex) { cv = null; }
            }
            else
            {
                //Annars hämtas användarinformation baserat på det angivna användar-ID:t
                user = _context.Users.Where(u => u.Id.Equals(userId)).Single();
                address = _context.Addresses.Where(a => a.UserId == userId).Single();
                try
                {
                    //Försöker hämta CV-informationen för den inloggade användaren
                    cv = _context.Cvs.Where(c => c.User_ID == userId).Single();
                }
                catch(Exception ex) { cv = null; }
            }
            

            //Skapar ett nytt ProfileViewModel-objekt
            var profileViewModel = new ProfileViewModel
            {
                User = user,
                Cv = cv,
                AuthorizedUserId = currentUserId,
                Address = address

            };
            return View(profileViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UploadFile()
        { 
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult UploadFile(ProfileViewModel profile)
        {
            //Kontrollerar om det finns en fil att ladda upp
            if (profile.ImageFile != null)
            {
                //Skapar sökväg för uppladdning av filen till mappen "Images" i wwwroot
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                //Kontrollerar om mappen inte finns och skapar den 
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                //Skapar ett unikt filnamn för den uppladdade filen
                string fileName = Guid.NewGuid().ToString() + "-" + profile.ImageFile.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                //Sparar filen på servern
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profile.ImageFile.CopyTo(fileStream);
                }

                // Hämtar användaren baserat på inloggad användares användarnamn
                User anv = _context.Users.Where(c => c.UserName.Equals(User.Identity.Name)).FirstOrDefault();

                //Hämtar användaren baserat på inloggad användares användarnamn
                String PhotoPath = fileName;
                anv.ProfilePicture = fileName;
                _context.Update(anv);
                _context.SaveChanges();
            }
            //Omdirigerar till profilsidan efter uppladdningen
            return RedirectToAction("Index", "Profile");
        }


        [HttpPost]
        [Authorize]
        public IActionResult UpdatePrivateStatus()
        {
            //Hämtar användaren baserat på inloggad användares användarnamn
            var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            //Kontrollerar och uppdaterar användarens privatstatus beroende på dess nuvarande status
            if (!user.PrivateStatus)
            {
                user.PrivateStatus = true;

                //Uppdaterar användarens status i databasen
                _context.Update(user);
                _context.SaveChanges();
            }
            else
            {
                user.PrivateStatus = false;

                //Uppdaterar användarens status i databasen
                _context.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Profile");
        }


        [HttpPost]
        [Authorize]
        public IActionResult UpdateIsDeactivated()
        {
            //Hämtar användaren baserat på inloggad användares användarnamn
            var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            //Kontrollerar och uppdaterar användarens privatstatus beroende på dess nuvarande status
            if (!user.IsDeactivated)
            {
                user.IsDeactivated = true;

                //Uppdaterar användarens status i databasen
                _context.Update(user);
                _context.SaveChanges();
            }
            else
            {
                user.IsDeactivated = false;

                //Uppdaterar användarens status i databasen
                _context.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Profile");
        }

        [HttpPost]
        public IActionResult ExportProfileToXml(string userId)
        {
            if(userId == null)
            {
                return NotFound();
            }
            

            var user = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
            user.PasswordHash = null;

            string path = user.UserName + ".xml";

            //Kontrollerar om mappen inte finns och skapar den 
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            Address address = _context.Addresses.Where(a => a.UserId == userId).FirstOrDefault();
            Cv? cv = _context.Cvs.Where(c => c.User_ID == user.Id).FirstOrDefault();
            List<CvCompetence> cvCompetences = new List<CvCompetence>();
            List<Competence> competences = new List<Competence>();
            List<CvEducation> cvEducations = new List<CvEducation>();
            List<Education> educations = new List<Education>();
            List<CvExperience> cvExperiences = new List<CvExperience>();
            List<Experience> experiences = new List<Experience>();
            List<Project> projects = new List<Project>();
            if(cv != null)
            {
                cvCompetences = _context.CvCompetences.Where(cc => cc.CvId == cv.Id).ToList();
                foreach(var cvCompetence in cvCompetences)
                {
                    competences.Add(_context.Competences.Where(c => c.Id == cvCompetence.CompetenceId).Single());
                }
                cvEducations = _context.CvEducations.Where(ce => ce.CvId == cv.Id).ToList();
                foreach (var cvEducation in cvEducations)
                {
                    educations.Add(_context.Educations.Where(c => c.Id == cvEducation.EducationId).Single());
                }
                cvExperiences = _context.CvExperiences.Where(cc => cc.CvId == cv.Id).ToList();
                foreach (var cvExperience in cvExperiences)
                {
                    experiences.Add(_context.Experiences.Where(c => c.Id == cvExperience.ExperienceId).Single());
                }
            }

            try
            {
                projects = _context.Projects.Where(p => p.CreatedByUserId.Equals(userId)).ToList();
            }
            catch (Exception ex) { }

            ExportProfileViewModel exportProfileViewModel = new ExportProfileViewModel { user = user,
                address = address, cv = cv, cvCompetences = cvCompetences, competences = competences, 
                cvEducations = cvEducations, educations = educations, cvExperiences = cvExperiences, 
                experiences = experiences, projects = projects };
            var data = exportProfileViewModel;

            XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
            var memoryStream = new System.IO.MemoryStream();
            
            xmlSerializer.Serialize(memoryStream, data);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return File(memoryStream, "CV_ASPMVC_GROUP2/xml", path);
        }

        //var newStream = new System.IO.MemoryStream();
        //var writer = new System.IO.StreamWriter(newStream);
        //writer.Write(xml);
        //writer.Flush();
        //newStream.Position = 0;

        //return File(newStream, "application/xml", "prova.xml");



    }
}
