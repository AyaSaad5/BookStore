using BookStore.BLL.Interfaces;
using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Repositories
{
    public class BookRepository : GenericRepository<Book> , IBookRepository
    {
        public BookRepository(BookStoreDbContext dbContext) : base(dbContext) 
        {

        }

    }
}
