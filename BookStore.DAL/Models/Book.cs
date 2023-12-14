using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public double Price5 { get; set; }
        public double Price10 { get; set; }
        public string ImageName { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; } // Restrict

        public Category Category { get; set; } // nav prop [one]

        [ForeignKey("Cover")]
        public int? CoverTypeId { get; set; }
        public CoverType CoverType { get; set; } // nav prop [one]
    }
}
