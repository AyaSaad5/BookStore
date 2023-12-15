using BookStore.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BookStore.PL.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime DateTimeOfCreation { get; set; }
        public ICollection<Book> Books { get; set; } // navigational property [many]
    }
}
