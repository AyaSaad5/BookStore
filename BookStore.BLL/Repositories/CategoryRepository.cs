using BookStore.BLL.Interfaces;
using BookStore.DAL;
using BookStore.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Repositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(BookStoreDbContext dbContext ): base(dbContext) 
        { 

        }
    }
}
