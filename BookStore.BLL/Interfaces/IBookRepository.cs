using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> GetBookByName(string title);
    }
}
