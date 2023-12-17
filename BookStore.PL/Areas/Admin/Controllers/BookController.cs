using AutoMapper;
using BookStore.BLL.Interfaces;
using BookStore.BLL.Repositories;
using BookStore.DAL.Models;
using BookStore.PL.Helpers;
using BookStore.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.PL.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Book> books;
            if(string.IsNullOrEmpty(SearchValue))
                books = await _unitofwork.BookRepository.GetAllAsync();
            else
                books =  _unitofwork.BookRepository.GetBookByName(SearchValue);
          
            var MappedBooks = _mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
            return View(MappedBooks);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel bookvm)
        {
            if (ModelState.IsValid)
            {
                bookvm.ImageName = DocumentSetting.UploadFile(bookvm.Image, "Images");
                var MappedBook = _mapper.Map<BookViewModel, Book>(bookvm);
                await _unitofwork.BookRepository.CreateAsync(MappedBook);
                int result = await _unitofwork.CompleteAsync();
                if (result > 0)
                    TempData["Message"] = "Book Added Sucessfully";
                return RedirectToAction(nameof(Index));
            }
            return View(bookvm);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var book = await _unitofwork.BookRepository.GetByIdAsync(id.Value);
            if (book is null)
                return NotFound();
            var MappedBook = _mapper.Map<Book, BookViewModel>(book);
            return View(ViewName, MappedBook);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel bookvm, [FromRoute] int id)
        {
            if(id != bookvm.Id) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    if (bookvm.Image != null)
                        bookvm.ImageName = DocumentSetting.UploadFile(bookvm.Image, "Images");
                    var MappedBook = _mapper.Map<BookViewModel,Book> (bookvm);
                    _unitofwork.BookRepository.Update(MappedBook);
                    int result = await _unitofwork.CompleteAsync();
                    if (result > 0)
                        TempData["Message"] = "Edit Complete Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty ,ex.Message);
                }
            }
            return View(bookvm);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task< IActionResult> Delete(BookViewModel bookvm, [FromRoute] int id)
        {
            if(id != bookvm.Id) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var MappedBook = _mapper.Map<BookViewModel, Book>(bookvm);
                    _unitofwork.BookRepository.Delete(MappedBook);
                    int result = await _unitofwork.CompleteAsync();
                    if(result > 0 && bookvm.Image is not null)
                    {
                        TempData["Message"] = "Deleted Successfully";
                        DocumentSetting.DeleteFile(bookvm.ImageName, "Images");
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(bookvm);
        }
    }
}
    
