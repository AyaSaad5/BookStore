using AutoMapper;
using BookStore.BLL.Interfaces;
using BookStore.DAL.Models;
using BookStore.PL.Models;
using BookStore.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.PL.Areas.Customer.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitofwork, IMapper mapper)
        {
            _logger = logger;
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task< IActionResult> Index()
        {
            var books =await  _unitofwork.BookRepository.GetAllAsync();
            var MappedBooks = _mapper.Map<IEnumerable< Book>,IEnumerable<BookViewModel>>(books);
            foreach (var book in MappedBooks)
                book.CommessionPrice = book.Price * 0.2;
            
            return View(MappedBooks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task< IActionResult> Details(int ? id)
        {
            if (id is null)
                return BadRequest();
            var books = await _unitofwork.BookRepository.GetAllAsync();
            if (books is null)
                return NotFound();
            var book = books.Where(b => b.Id == id).FirstOrDefault();
            var MappedBook = _mapper.Map<Book, BookViewModel>(book);
            return View(MappedBook);
        }
    }
}
