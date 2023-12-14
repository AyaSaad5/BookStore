﻿using BookStore.BLL.Interfaces;
using BookStore.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBookRepository BookRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public ICoverTypeRepository CoverTypeRepository { get; set; }

        public UnitOfWork(BookStoreDbContext dbContext)
        {
            BookRepository = new BookRepository(dbContext);
            CategoryRepository = new CategoryRepository(dbContext);
            CoverTypeRepository = new CoverTypeRepository(dbContext);

        }
    }
}