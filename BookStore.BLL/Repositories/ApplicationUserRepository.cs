﻿using BookStore.BLL.Repositories;
﻿using BookStore.BLL.Interfaces;
using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.BLL.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser> , IApplicationUserRepository
	{
        private readonly BookStoreDbContext _dbContext;
        public ApplicationUserRepository(BookStoreDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
