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
    public class CoverTypeRepository : GenericRepository<CoverType> , ICoverTypeRepository
    {
        public CoverTypeRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
