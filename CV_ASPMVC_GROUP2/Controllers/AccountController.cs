using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CV_ASPMVC_GROUP2.Models;
using Microsoft.EntityFrameworkCore;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class AccountController : Controller
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
            if (ModelState.IsValid)
            {
                User anvandare = new User();
                anvandare.UserName = registerViewModel.UserName;
                var result =
                await userManager.CreateAsync(anvandare, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(anvandare, isPersistent: true);
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
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
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
                    ModelState.AddModelError("", "Fel användarnam/lösenord.");
                }
            }
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword (ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("LogIn");
                }

             
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

                    await signInManager.RefreshSignInAsync(user);
                    TempData["SuccessMessage"] = "Ditt lösenord har ändrats.";
                    return View("ChangePassword");
                }
            }

            return View(changePasswordViewModel);
        }
    }
    }


