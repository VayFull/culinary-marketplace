using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Subs.Domain.Entities;
using Subs.Infrastructure.Contexts;
using Subs.Services;

namespace Subs.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SubsDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly CarService _cars;
        private readonly RecipeService _recipes;

        public HomeController(ILogger<HomeController> logger, SubsDbContext context, CarService service, RecipeService recipe)
        {
            _cars = service;
            _logger = logger;
            _context = context;
            _recipes = recipe;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult AddRecipe()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecipe(Recipe recipe)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("someController/somePage")]
        public IActionResult SomePage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Books()
        {
            return View(_context.Books);
        }

        [HttpPost]
        public IActionResult Books(string search)
        {
            if (search == null || search == "")
                return View(_context.Books);
            var normalizedSearch = search.ToUpper();
            return View(_context.Books.Where(x => x.Author.ToUpper().Contains(normalizedSearch) || x.Name.ToUpper().Contains(normalizedSearch)).ToList());
        }

        public IActionResult DownloadBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return NotFound();
            string fileName = book.FileName;
            string filePath = GetPath() + fileName;

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }

        private string GetPath()
        {
            var typeOfController = GetType();
            var path = typeOfController.Assembly.Location;
            var filePath = path.Remove(path.Length - 37, 37);

            filePath += @"Files\\";

            return filePath;
        }

        public IActionResult Api()
        {
            return View();
        }

        public IActionResult GetApiKey()
        {
            var user = User.Identity.Name;
            var userKey = _context.UserKeys.Find(user);
            return View(userKey);
        }

        public IActionResult GetKey()
        {
            var user = User.Identity.Name;
            if (_context.UserKeys.Find(user) == null)
            {
                var currentTime = DateTime.Now;
                var notHashedKey = user + new Random().Next() + currentTime;
                var key = HashService.GetHashString(notHashedKey);
                _context.UserKeys.Add(new UserKey { Key = key, Username = user });
                _context.SaveChanges();
            }

            return RedirectToAction("GetApiKey");
        }
    }
}
