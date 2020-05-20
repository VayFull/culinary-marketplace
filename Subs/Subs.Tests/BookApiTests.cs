using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Subs.Controllers;
using Subs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Subs.Tests
{
    public class BookApiTests : IClassFixture<SharedDatabase>
    {
        public SharedDatabase Context { get; }
        public BookApiTests(SharedDatabase context)
        {
            Context = context;
        }

        [Fact]
        public async void Try_Get_Books_With_Random_Key()
        {
            using (var context = Context.CreateContext())
            {
                var controller = new BooksController(context);

                var books = await controller.GetBooks("123");

                Assert.False(books.Result is IEnumerable<Book>);
            }
        }

        [Fact]
        public async void Try_Get_Books_With_Success_Key()
        {
            using (var context = Context.CreateContext())
            {
                var controller = new BooksController(context);
                var someUserKey = context.UserKeys.FirstOrDefault();
                var key = "";
                if (someUserKey != null)
                {
                    key = someUserKey.Key;
                }

                var books = await controller.GetBooks(key);

                Assert.True(books.Value.Any());
            }
        }

        [Fact]
        public async void Try_Post_Book_Wrong_Book()
        {
            using (var context = Context.CreateContext())
            {
                var previousCount = context.Books.Count();
                var controller = new BooksController(context);
                var someWrongBook = new Book { Author = null, Name = "" };
                await controller.PostBook(someWrongBook);
                Assert.Equal(previousCount, context.Books.Count());
            }
        }

        [Fact]
        public async void Try_Post_Book_Right_Book()
        {
            using (var context = Context.CreateContext())
            {
                var previousCount = context.Books.Count();
                var controller = new BooksController(context);
                var someOkBook = new Book { Author = "someAuthor", Name = "someName", FileName = "someFileName", Id = 234 };
                await controller.PostBook(someOkBook);
                Assert.Equal(previousCount + 1, context.Books.Count());
            }
        }
    }
}
