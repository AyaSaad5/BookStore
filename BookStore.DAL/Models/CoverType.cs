using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Models
{
    public class CoverType
    {
        public int CoverTypeId { get; set; }

        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
      
    }
}
