using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using todo.Models;
using todo.ViewModels;

namespace todo
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ModelContext _context;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ModelContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(new IndexViewModel
            {
                ExistingTodos = _context.Todos.ToList(),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public IActionResult Index(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Todos.FirstOrDefault(e => e.value == model.NewTodo) != null)
                {
                    ModelState.AddModelError(nameof(model.NewTodo), "An existing todo element has the same name.");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Todos.Add(new Todo
                {
                    value = model.NewTodo,
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(HomeController.Index));
            }
            if (model.ExistingTodos == null)
            {
                model.ExistingTodos = _context.Todos.ToList();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveNote([FromRoute] int id)
        {
            var todo = _context.Todos
                .FirstOrDefault(e => e.ID == id);
            if (todo == null)
            {
                // Already deleted or never existed.
                return NotFound();
            }
            _context.Remove(todo);
            _context.SaveChanges();
            return RedirectToAction(nameof(HomeController.Index));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginUser());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            var user = new ApplicationUser();
            if (ModelState.IsValid)
            {
                user.UserName = model.Username;
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ModelState.AddModelError("Password", "Username and password does not match");
                }
            }
            if (ModelState.IsValid)
            {
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction(nameof(HomeController.Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(LoginUser model)
        {
            return View(new RegisterUser());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.SingleOrDefault(
                    e => e.UserName.ToLower() == registerUser.Username.ToLower()) != null)
                {
                    ModelState.AddModelError("Username", "Username already exists, pick another one.");
                }
                if (registerUser.Password != registerUser.Password2)
                {
                    ModelState.AddModelError("Password", "The passwords are not equal.");
                }
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = registerUser.Username };
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction(nameof(HomeController.Index));
                }
                // Dangerous ? =)
                ModelState.AddModelError("Username", result.Errors.First().Code);
            }
            return View(registerUser);
        }
    }
}