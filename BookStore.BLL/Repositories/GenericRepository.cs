using BookStore.BLL.Interfaces;
using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookStoreDbContext _dbContext;

        public GenericRepository(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(T item)
        {
            await _dbContext.AddAsync(item);
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public  void Update(T item)
        {
             _dbContext.Update(item);
        }
    }
}
