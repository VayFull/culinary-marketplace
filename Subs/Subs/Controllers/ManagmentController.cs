using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Subs.Domain.Entities;
using Subs.Domain.Entities.ViewModels;
using Subs.Infrastructure.Contexts;

namespace Subs.Controllers
{
    [Authorize(Roles = "admin")]
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

        public IActionResult Book()
        {
            var books = _context.Books;
            return View(books);
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    book.FileName = file.FileName;
                    var json = JsonSerializer.Serialize(book);

                    using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        var response = client.PostAsync("https://localhost:44332/api/Books", stringContent);
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            CreateFile(file);
                            return RedirectToAction("Book");
                        }
                        else
                            return View(book);
                    }
                }
            }
            else
            {
                return View(book);
            }
        }

        private async void CreateFile(IFormFile file)
        {
            var filePath = GetPath();
            filePath += file.FileName;

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
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
    }
}