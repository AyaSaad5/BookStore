using BookStore.BLL.Interfaces;
using BookStore.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PL.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var CoverTypes = await _unitOfWork.CoverTypeRepository.GetAllAsync();
            return View(CoverTypes);
        }


        #region Create
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoverType cover)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.CoverTypeRepository.CreateAsync(cover);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cover);
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            var CoverType = await _unitOfWork.CoverTypeRepository.GetByIdAsync(id);
            return View(CoverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id ,CoverType cover )
        {
            if (cover.CoverTypeId != id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CoverTypeRepository.Update(cover);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(cover);
                }
            }
            return View(cover);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int id) 
        {
            var cover = await _unitOfWork.CoverTypeRepository.GetByIdAsync(id);
            return View(cover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, CoverType cover)
        {
            if (id != cover.CoverTypeId)
                return BadRequest();

            try
            {
                _unitOfWork.CoverTypeRepository.Delete(cover);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(cover);
            }

        }
        #endregion
    }
}
