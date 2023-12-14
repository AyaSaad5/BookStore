using BookStore.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Pharmacy.PL.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;

        public DashboardController(IUnitOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }
        public IActionResult Index()
        {
            return View();
        }


      
    }
}
