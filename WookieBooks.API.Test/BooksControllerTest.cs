using System;
using Xunit;
using WookieBooks.API.Controllers;
using WookieBooks.API.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Moq;
using WookieBooks.API.Domain.Services;
using WookieBooks.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using WookieBooks.API.Persistence.Contexts;
using WookieBooks.API.Persistence.Repositories;
using WookieBooks.API.Services;
using System.Threading.Tasks;
using System.Linq;

namespace WookieBooks.API.Test
{
    public class BooksControllerTest
    {
        private readonly BooksController _booksController;

        public BooksControllerTest()
        {
            // Arrange
            var databaseOptionsBuilder = new DbContextOptionsBuilder<WookieBooksDBContext>().UseInMemoryDatabase("WookieBooksDBContext");
            var database = new WookieBooksDBContext(databaseOptionsBuilder.Options);
            database.Database.EnsureDeleted();
            database.Database.EnsureCreated();

            var bookRepository = new BookRepository(database);
            var bookService = new BookService(bookRepository);
            var mockthis = new Mock<IBookService>();
            _booksController = new BooksController(bookService);
        }

        [Fact]
        public void GetTest_ReturnsListOfBooks()
        {
            // Act
            var result = _booksController.GetAllBooks().Result;

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetTest_ReturnsBookById()
        {
            // Act
            int bookId = 100;
            var result = _booksController.GetBookById(bookId).Result;

            // Assert
            Assert.Equal(bookId, result.Value.Id);
        }

        [Fact]
        public void GetTest_ReturnsBookById_Fails()
        {
            // Act
            int bookId = 120;
            var result = _booksController.GetBookById(bookId).Result.Result as NotFoundResult;

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void DeleteTest_DeletesBookById()
        {
            // Act
            int bookId = 100;
            var result = _booksController.DeleteBook(bookId).Result.Result as OkObjectResult;
            var book = result.Value as Book;
            // Assert
            Assert.Equal(bookId, book.Id);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteTest_DeletesBookById_Fails()
        {
            // Act
            int bookId = 1000;
            var result = _booksController.DeleteBook(bookId).Result.Result as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Book does not exist.", result.Value);
        }

        [Fact]
        public void PostTest_CreatesBook()
        {
            // Act
            var book = new Book
            {
                Id = 130,
                Title = "A Title",
                Author = "An Author",
                Description = "A Description",
                CoverImage = "https://www.animage.com",
                Price = 101.01

            };
            var result = _booksController.CreateBook(book).Result.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(book, result.Value);
        }

        [Fact]
        public void PostTest_CreatesBook_Fails()
        {
            // Act
            var book = new Book
            {
                Id = 100,
                Title = "A Title",
                Author = "An Author",
                Description = "A Description",
                CoverImage = "https://www.animage.com",
                Price = 101.01

            };
            var result = _booksController.CreateBook(book).Result.Result as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("An item with the same key has already been added.", (string)result.Value);
        }

        [Fact]
        public void PostTest_CreatesBook_FailsDueToInvalid()
        {
            // Act
            var book = new Book
            {
                Id = 150,
                Description = "A Description",
                CoverImage = "https://www.animage.com",

            };
            _booksController.ModelState.AddModelError("Error", "Invalid Data");

            // Act
            var result = _booksController.CreateBook(book).Result.Result as BadRequestObjectResult;
            var value = (List<string>)result.Value;

            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("Invalid Data", value[0]);
        }
        
        [Fact]
        public void PutTest_UpdatesBook()
        {
            // Act
            var bookId = 100;
            var book = new Book
            {
                Id = 100,
                Title = "A Title",
                Author = "An Author",
                Description = "A Description",
                CoverImage = "https://www.animage.com",
                Price = 101.01

            };
            var result = _booksController.UpdateBook(bookId, book).Result.Result as OkObjectResult;
            var getBook = _booksController.GetBookById(bookId).Result;

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(book, result.Value);
            Assert.NotEqual("Green eggs and ham", getBook.Value.Title);
        }

        [Fact]
        public void PutTest_UpdatesBook_Fails()
        {
            // Act
            var bookId = 130;
            var book = new Book
            {
                Id = 130,
                Title = "A Title",
                Author = "An Author",
                Description = "A Description",
                CoverImage = "https://www.animage.com",
                Price = 101.01

            };
            var result = _booksController.UpdateBook(bookId, book).Result.Result as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Book does not exist.", result.Value);
        }

        [Fact]
        public void PutTest_UpdatesBook_FailsDueToIdMismatch()
        {
            // Act
            var bookId = 120;
            var book = new Book
            {
                Id = 130,
                Title = "A Title",
                Author = "An Author",
                Description = "A Description",
                CoverImage = "https://www.animage.com",
                Price = 101.01

            };
            var result = _booksController.UpdateBook(bookId, book).Result.Result as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Update id does not match id of supplied book object.", result.Value);
        }
    }
}
