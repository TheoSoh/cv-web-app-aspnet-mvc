using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class AccountController : BaseController
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private TestDbContext testDbContext;
        public AccountController(UserManager<User> userMngr,
        SignInManager<User> signInMngr, TestDbContext context)
        {
            this.userManager = userMngr;
            this.signInManager = signInMngr;
            this.testDbContext = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid) //Kollar att modellen är giltig
            {
                //Skapar en ny användare och tilldelar egenskaper
                User user = new User();
                user.UserName = registerViewModel.UserName;
                user.FirstName = registerViewModel.FirstName;
                user.LastName = registerViewModel.LastName;
                user.PhoneNumber = registerViewModel.PhoneNumber;
                user.Email = registerViewModel.Email;

                //Skapar användaren i databasen
                var result =await userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    //Om användaren lyckades skapas skapar vi en adress till användaren
                    var address = new Address { User = user };
                    testDbContext.Add(address);
                    testDbContext.SaveChanges();

                    //Loggar in användaren
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            //Skapar en ny instans av LoginViewModel som kan användas i vyn
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                //Försöker logga in användaren med angivna uppgifter
                var result = await signInManager.PasswordSignInAsync(
                loginViewModel.UserName,
                loginViewModel.Password,
                isPersistent: loginViewModel.RememberMe,
                lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Felmeddelande om inloggningen misslyckas
                    ModelState.AddModelError("", "Fel användarnam/lösenord.");
                }
            }
            return View(loginViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            //Loggar ut användaren
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Search(string searchString)
        {
            //Hämtar användare från databasen
            var users = from u in testDbContext.Users select u;

            //Om söksträngen inte är null eller tom, filtrera efter användarnamn
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString));
            }

            //Hämta resultat från sökning och returnera till vyn "Users"
            var searchResult = await users.ToListAsync();
            return View("Users");
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                //Hämtar den aktuella användaren från inloggningen
                var user = await userManager.GetUserAsync(User);

                //Om användare är null omdirigerar vi till vyn för att logga in
                if (user == null)
                {
                    return RedirectToAction("LogIn");
                }

                //Ändra det befintliga lösenordet till ett nytt
                var result = await userManager.ChangePasswordAsync(user,
                changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                else
                {
                    //Uppdaterar inloggningsuppgifterna för användaren
                    await signInManager.RefreshSignInAsync(user);

                    //Temporärt meddelande för att bekräfta lösenordsändringen och visar vyn för lösenordsändring
                    TempData["SuccessMessage"] = "Ditt lösenord har ändrats.";
                    return View("ChangePassword");
                }
            }

            return View(changePasswordViewModel);
        }



        [HttpGet]
        [Authorize]
        public IActionResult EditUser()
        {
            //Hämtar den inloggade användarens information från databasen baserat på användarnamnet
            var anv = testDbContext.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            var model = new EditUserViewModel
            {
                FirstName = anv.FirstName,
                LastName = anv.LastName,
                Email = anv.Email,
                PhoneNumber = anv.PhoneNumber

            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            //Hämtar den inloggande användaren
            var user = await userManager.GetUserAsync(User);

            //Uppdaterar användaren med den nya informationen
            user.FirstName = editUserViewModel.FirstName;
            user.LastName = editUserViewModel.LastName;
            user.Email = editUserViewModel.Email;
            user.PhoneNumber = editUserViewModel.PhoneNumber;

            //Uppdaterar användaren i databasen
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "home");
            }

            //Om uppdateringen misslyckas, returnera vyn för att fortsätta redigera användarinformationen
            return View(editUserViewModel);
        }
    }
}

