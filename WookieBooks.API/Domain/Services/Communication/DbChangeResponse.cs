using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.API.Domain.Models;

namespace WookieBooks.API.Domain.Services.Communication
{
    public class DbChangeResponse
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public Book Book { get; private set; } 

        public DbChangeResponse(bool success, string message, Book book)
        {
            Success = success;
            Message = message;
            Book = book;
        }
    }
}
