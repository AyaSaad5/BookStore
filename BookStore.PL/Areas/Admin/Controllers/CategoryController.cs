using AutoMapper;
using BookStore.BLL.Interfaces;
using BookStore.BLL.Repositories;
using BookStore.DAL;
using BookStore.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.PL.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitofwork, IMapper mapper) //Ask clr to create obj from class which implement ICategoryRepository
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _unitofwork.CategoryRepository.GetAllAsync();
            var MappedCategories = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);
            return View(MappedCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryvm)
        {
            if (ModelState.IsValid)
            {
                var MappedCategory = _mapper.Map<CategoryViewModel, Category>(categoryvm);
                await _unitofwork.CategoryRepository.CreateAsync(MappedCategory);
                int result = await _unitofwork.CompleteAsync();
                if (result > 0)
                    TempData["Message"] = "Category is Created ";
                return RedirectToAction(nameof(Index));
            }
            return View(categoryvm);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var category = await _unitofwork.CategoryRepository.GetByIdAsync(id.Value);
            if (category is null)
                return NotFound();
            var MappedCategory = _mapper.Map<Category,CategoryViewModel>(category);
            return View(ViewName, MappedCategory);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel categoryvm, [FromRoute] int id)
        {
            if (id != categoryvm.CategoryId)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedCategory = _mapper.Map<CategoryViewModel, Category>(categoryvm);
                    _unitofwork.CategoryRepository.Update(MappedCategory);
                    int result = await _unitofwork.CompleteAsync();
                    if (result > 0)
                        TempData["Message"] = "Edit Complete Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //1. log Exception
                    //2. form
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(categoryvm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryViewModel categoryvm, [FromRoute] int id)
        {
            if (id != categoryvm.CategoryId)
                return BadRequest();
            try
            {
                var MappedCategory = _mapper.Map<CategoryViewModel, Category>(categoryvm);
                _unitofwork.CategoryRepository.Delete(MappedCategory);
                int result = await _unitofwork.CompleteAsync();
                if (result > 0)
                    TempData["Message"] = "Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(categoryvm);
            }
        }

    }
}
