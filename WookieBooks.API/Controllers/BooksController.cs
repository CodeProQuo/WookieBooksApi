using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WookieBooks.API.Domain.Services;
using WookieBooks.API.Domain.Models;
using WookieBooks.API.Extensions;

namespace WookieBooks.API.Controllers
{
    [Route("/api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookService.ListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _bookService.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("Update id does not match id of supplied book object.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _bookService.UpdateAsync(id, book);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _bookService.AddAsync(book);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Book);
        }
    }
}
