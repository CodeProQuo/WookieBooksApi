using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.API.Persistence.Contexts;
using WookieBooks.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using WookieBooks.API.Domain.Models;
using WookieBooks.API.Domain.Services.Communication;

namespace WookieBooks.API.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {

        protected readonly WookieBooksDBContext _context;

        public BookRepository(WookieBooksDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task UpdateAsync(Book book)
        {
            var existingBook = _context.Books.First(b => b.Id == book.Id);
            _context.Entry(existingBook).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
