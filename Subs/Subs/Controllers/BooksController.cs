using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Subs.Domain.Entities;
using Subs.Infrastructure.Contexts;

namespace Subs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly SubsDbContext _context;

        public BooksController(SubsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string key)
        {
            if (_context.UserKeys.Where(x => x.Key == key).Any())
            {
                return await _context.Books.ToListAsync();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (String.IsNullOrEmpty(book.Author) || String.IsNullOrEmpty(book.FileName) || String.IsNullOrEmpty(book.Name))
                return BadRequest();
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }
    }
}
