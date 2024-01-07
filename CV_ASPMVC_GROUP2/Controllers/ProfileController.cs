using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public IActionResult Index(string userId)
        {
            //Hämtar den aktuella inloggade användarens ID
            string currentUserId = base.UserId;

            //Skapar ett nytt ProfileViewModel-objekt
            var profileViewModel = new ProfileViewModel
            {
                AuthorizedUserId = currentUserId
            };

            //Om användar-ID inte är specificerat används den inloggade användarens ID för att hämta dess information
            if (userId == null)
            {
                profileViewModel.User = _context.Users.Where(u => u.Id.Equals(currentUserId)).Single(); 
            }
            else
            {
                //Annars hämtas användarinformation baserat på det angivna användar-ID:t
                profileViewModel.User = _context.Users.Where(u => u.Id.Equals(userId)).Single();
            }
            try
            {
                //Försöker hämta CV-informationen för den inloggade användaren
                profileViewModel.Cv = _context.Cvs.Where(c => c.User_ID == currentUserId).Single();
            }
            catch(Exception ex) { profileViewModel.Cv = null; }
            return View(profileViewModel);
        }

        [HttpGet]
        public IActionResult UploadFile()
        { 
            return View();
        }

        [HttpPost]
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
            var anv = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            //Kontrollerar och uppdaterar användarens privatstatus beroende på dess nuvarande status
            if (!anv.PrivateStatus)
            {
                anv.PrivateStatus = true;

                //Uppdaterar användarens status i databasen
                _context.Update(anv);
                _context.SaveChanges();
            }
            else
            {
                anv.PrivateStatus = false;

                //Uppdaterar användarens status i databasen
                _context.Update(anv);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Profile");
        }
    }
}
