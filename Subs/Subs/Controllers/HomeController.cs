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
    }
}
