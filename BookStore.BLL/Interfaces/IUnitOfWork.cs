using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IBookRepository BookRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public ICoverTypeRepository CoverTypeRepository { get; set; }
    }
}
