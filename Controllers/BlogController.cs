using Kurs.Data;
using Kurs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Kurs.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }

        // GET: Blog
        public IActionResult Index()
        {
            return View(@"~/Views/Home/Blog.cshtml");
        }

        public IActionResult CreateTable(string title, string text)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                ViewBag.ErrorMessage = "Name cannot be empty.";
                return View(@"~/Views/Home/Blog.cshtml");
            }

            var existingUser = _context.Articles.FirstOrDefault(u => u.Title == title);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Article with this title already exists.";
                return View(@"~/Views/Home/Blog.cshtml");
            }

            var article = new Article
            {
                Title = title,
                Text = text
            };
            _context.Articles.Add(article);
            _context.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, string title, string text)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            // Обновляем данные статьи
            article.Title = title;
            article.Text = text;
            _context.SaveChanges();

            // Перенаправляем на действие Index
            return RedirectToAction("Index");
        }

    }
}
