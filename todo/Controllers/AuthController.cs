using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using todo.Models;
using todo.ViewModels;

namespace todo {
    public class AuthController : Controller {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ModelContext _context;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ModelContext context) {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() {
            return View(new LoginUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser model) {
            if (ModelState.IsValid) {
                var user = _userManager.Users.SingleOrDefault(u => u.UserName.ToLower() == model.Username.ToLower());
                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password)) {
                    ModelState.AddModelError(string.Empty, "Username and password does not match");
                    return View(model);
                }
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() {
            return View(new RegisterUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUser model) {
            if (ModelState.IsValid) {
                if (_context.Users.SingleOrDefault(
                    e => e.UserName.ToLower() == model.Username.ToLower()) != null) {
                    ModelState.AddModelError("Username", "Username already exists, pick another one.");
                    return View(model);
                }
                if (model.Password != model.Password2) {
                    ModelState.AddModelError(string.Empty, "The passwords are not equal.");
                    return View(model);
                }
                var user = new ApplicationUser { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                // Dangerous ? =)
                ModelState.AddModelError("Username", result.Errors.First().Code);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}