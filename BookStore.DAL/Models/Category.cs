using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime DateTimeOfCreation { get; set; }
        public ICollection<Book> Books { get; set; } // navigational property [many]
    }
}
