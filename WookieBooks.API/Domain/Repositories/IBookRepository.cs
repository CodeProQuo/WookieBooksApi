using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.API.Domain.Models;
using WookieBooks.API.Domain.Services.Communication;

namespace WookieBooks.API.Domain.Repositories
{
    public interface IBookRepository
    {

        Task<IEnumerable<Book>> ListAsync();
        Task<Book> FindByIdAsync(int id);
        Task UpdateAsync(Book book);
        Task AddAsync(Book book);
        Task DeleteAsync(Book book);
       
    }
}
