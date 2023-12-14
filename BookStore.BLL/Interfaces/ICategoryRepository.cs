using BookStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }
}
