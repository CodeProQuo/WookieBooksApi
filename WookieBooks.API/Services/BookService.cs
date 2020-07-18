using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.API.Domain.Models;
using WookieBooks.API.Domain.Services;
using WookieBooks.API.Domain.Repositories;
using WookieBooks.API.Domain.Services.Communication;

namespace WookieBooks.API.Services
{
    // Service class to implement business logic for books api.
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await _bookRepository.ListAsync();
        }

        public async Task<Book> FindAsync(int id)
        {
            return await _bookRepository.FindByIdAsync(id);
        }

        public async Task<DbChangeResponse> AddAsync(Book book)
        {
            try
            {
                await _bookRepository.AddAsync(book);
                return new DbChangeResponse(true, "Book succesfully added.", book);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the book: { ex.Message}");
                return new DbChangeResponse(false, $"An error occurred while creatting the book: {ex.Message}", book);
            }
        }

        public async Task<DbChangeResponse> UpdateAsync(int id, Book book)
        {
            if (await _bookRepository.FindByIdAsync(id) == null)
                return new DbChangeResponse(false, "Book does not exist.", book);

            try {
                await _bookRepository.UpdateAsync(book);
                return new DbChangeResponse(true, "Book succesfully updated.", book);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the book: { ex.Message}");
                return new DbChangeResponse(false, $"An error occurred while updating the book: {ex.Message}", book);
            }
        }

        public async Task<DbChangeResponse> DeleteAsync(int id)
        {
            var book = await _bookRepository.FindByIdAsync(id);
            if (book == null)
                return new DbChangeResponse(false, "Book does not exist.", book);

            try
            {
                await _bookRepository.DeleteAsync(book);
                return new DbChangeResponse(true, "Book succesfully deleted.", book);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the book: { ex.Message}");
                return new DbChangeResponse(false, $"An error occurred while deleting the book: {ex.Message}", book);
            }
        }
    }
}
