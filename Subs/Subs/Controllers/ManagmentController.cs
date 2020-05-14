using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subs.Domain.Entities;
using Subs.Domain.Entities.ViewModels;
using Subs.Infrastructure.Contexts;

namespace Subs.Controllers
{
    [Authorize(Roles ="admin")]
    public class ManagmentController : Controller
    {
        private readonly SubsDbContext _context;
        public ManagmentController(SubsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()
        {
            var categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = _context.Categories.ToList();
            return View(categoryViewModel);
        }

        [HttpPost]
        public IActionResult Category(CategoryViewModel categoryViewModel)
        {
            _context.Categories.Add(categoryViewModel.Category);
            _context.SaveChanges();
            categoryViewModel.Categories = _context.Categories.ToList();
            return View(categoryViewModel);
            
        }

        public IActionResult CategoryRemove(int id)
        {
            var element = _context.Categories.Find(id);
            _context.Remove(element);
            _context.SaveChanges();
            return RedirectToAction("Category");
        }
    }
}