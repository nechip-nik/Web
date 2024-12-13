using Kurs.Data;
using Kurs.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kurs.Controllers
{
    public class AutheficationController : Controller
    {
        private readonly BlogContext _context;

        public AutheficationController(BlogContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(@"~/Views/Home/Authefication.cshtml");
        }
        public IActionResult SignUp(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.ErrorMessage = "Name cannot be empty.";
                return View("~/Views/HomePage/Authentication.cshtml");
            }
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "User with this email already exists.";
                return View("~/Views/Home/Authentication.cshtml");
            }
            var user = new User
            {
                Name = name,
                Email = email,
                Password = password
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }
        public IActionResult SignIn(string email, string password)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser == null)
            {
                ViewBag.ErrorMessage = "User with this email does not exist.";
                return View("~/Views/Home/Authentication.cshtml");
            }
            if (existingUser.Password != password)
            {
                ViewBag.ErrorMessage = "Incorrect password. Please try again.";
                return View("~/Views/Home/Authentication.cshtml");
            }

            return RedirectToAction("Index", "Blog");
        }
    }
}
