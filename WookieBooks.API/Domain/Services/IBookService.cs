using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WookieBooks.API.Domain.Models;
using WookieBooks.API.Domain.Services.Communication;

namespace WookieBooks.API.Domain.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> ListAsync();
        Task<Book> FindAsync(int id);
        Task<DbChangeResponse> UpdateAsync(int id, Book book);
        Task<DbChangeResponse> AddAsync(Book book);
        Task<DbChangeResponse> DeleteAsync(int id);
    }
}
