using System;
using Microsoft.AspNetCore.Mvc;

namespace todo
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public IActionResult AddNote([FromForm] string value)
        {
            // Fetch value from POST, somehow.
            throw new Exception(value);
        }
    }
}