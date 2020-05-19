using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Subs.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "введите название книги")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите автора книги")]
        public string Author { get; set; }
        public string FileName { get; set; }
    }
}
