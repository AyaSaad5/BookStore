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
        private readonly BookStoreDbContext _dbContext;

        public BookRepository(BookStoreDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public IQueryable<Book> GetBookByName(string title)
        => _dbContext.Books.Where(B => B.Title.ToLower().Contains(title.ToLower()));
    }
}
