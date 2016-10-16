using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todo.Models;
using todo.ViewModels;

namespace todo
{
    public class HomeController : Controller
    {
        private TodoContext _context;

        public HomeController(TodoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["todos"] = _context.Todos.ToList();
            return View(_context.Todos.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public IActionResult AddNote([FromForm] AddTodo value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad request");
            }

            var todo = new Todo
            {
                value = value.value
            };
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return Redirect("/");
        }

        [HttpPost]
        [Route("{id:int}", Name = "RemoveTodo")]
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
    }
}