using BookStore.BLL.Repositories;
using BookStore.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UnitOfWork _unitofwork;

        public CategoryController(UnitOfWork unitofwork) //Ask clr to create obj from class which implement ICategoryRepository
        {
            _unitofwork = unitofwork;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _unitofwork.CategoryRepository.GetAllAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
                await _unitofwork.CategoryRepository.CreateAsync(category);
                int result = await _unitofwork.CompleteAsync();
                if (result > 0)
                    TempData["Message"] = "Category is Created ";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var category = await _unitofwork.CategoryRepository.GetByIdAsync(id.Value);
            if(category is null)
                return NotFound();
            return View(ViewName,category);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category, [FromRoute] int id)
        {
            if (id != category.CategoryId)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitofwork.CategoryRepository.Update(category);
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
            return View(category);
        }

        public async Task <IActionResult> Delete(int? id)
        {
            return await Details(id,"Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category category, [FromRoute] int id)
        {
            if (id != category.CategoryId)
                return BadRequest();
            try
            {
                _unitofwork.CategoryRepository.Delete(category);
                int result = await _unitofwork.CompleteAsync();
                if (result > 0)
                    TempData["Message"] = "Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(category);
            }
        }

    }
}
