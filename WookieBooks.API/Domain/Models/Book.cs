using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace WookieBooks.API.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        public string CoverImage { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
