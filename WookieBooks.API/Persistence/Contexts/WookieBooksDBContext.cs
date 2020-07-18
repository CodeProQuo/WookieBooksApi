using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using WookieBooks.API.Domain.Models;

namespace WookieBooks.API.Persistence.Contexts
{
    public class WookieBooksDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public WookieBooksDBContext(DbContextOptions<WookieBooksDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>().ToTable("Books");
            builder.Entity<Book>().HasKey(p => p.Id);
            builder.Entity<Book>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Book>().Property(p => p.Title).IsRequired();
            builder.Entity<Book>().Property(p => p.Author).IsRequired();
            builder.Entity<Book>().Property(p => p.Price).IsRequired();

            builder.Entity<Book>().HasData
            (
                new Book
                {
                    Id = 100,
                    Title = "Green Eggs and Ham",
                    Description = "'I like green eggs and ham.' says Sam I Am.",
                    Author = "Dr Seuss",
                    CoverImage = "https://images-na.ssl-images-amazon.com/images/I/61akeHxE0OL._SX258_BO1,204,203,200_.jpg",
                    Price = 45.67
                },
                new Book
                {
                    Id = 101,
                    Title = "The Godfather",
                    Description = "I'm gonna make him an offer he can't refuse.",
                    Author = "Mario Puzo",
                    CoverImage = "https://images-na.ssl-images-amazon.com/images/I/71zypXxMynL.jpg",
                    Price = 99.99
                }
            );
        }
    }
}
