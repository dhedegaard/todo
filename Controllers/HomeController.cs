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
            ViewData["todos"] = _context.Todos.ToList();
            return View(_context.Todos.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public IActionResult AddNote([FromForm] string value)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(value))
            {
                return BadRequest("Bad request");
            }

            var todo = new Todo
            {
                value = value
            };
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return Redirect("/");
        }

        [HttpPost]
        [Route("/todos/remove/{id:int}", Name = "RemoveTodo")]
        public IActionResult RemoveNote(int id)
        {
            var todo = _context.Todos
                .FirstOrDefault(e => e.ID == id);
            if (todo == null)
            {
                return BadRequest($"Todo object with ID {id} does not exist");
            }
            _context.Remove(todo);
            _context.SaveChanges();
            return Redirect("/");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            if (Request.Method == "POST" && ModelState.IsValid)
            {
                var user = new ApplicationUser{UserName = username};
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction(nameof(HomeController.Index), "Index");
                throw new Exception(username + " - " + password);
            }
            return View();
        }
    }
}