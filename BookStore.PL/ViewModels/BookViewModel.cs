using BookStore.DAL.Models;
using BookStore.DAL;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.PL.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Author is Required")]

        public string Author { get; set; }
        public double Price { get; set; }
        public double CommessionPrice { get; set; } =  0.2;

        public double Price5 { get; set; }
        public double Price10 { get; set; }
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; } // Restrict

        public Category Category { get; set; } // nav prop [one]

        [ForeignKey("Cover")]
        public int? CoverTypeId { get; set; }
        public CoverType CoverType { get; set; } // nav prop [one]
    }
}
