using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Contexts
{
    public class BookStoreDbContext :DbContext
    {
        public BookStoreDbContext(DbContextOptions <BookStoreDbContext> options) : base(options) 
        { 
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }

    }
}
