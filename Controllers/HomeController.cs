using GasHub.Models;
using GasHub.Models.ViewModels;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GasHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;
        public HomeController(ILogger<HomeController> logger , IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _logger = logger;
            _unitOfWorkClientServices=unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productsVm = new ProductViewModel();
            productsVm.ProductList = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            productsVm.companiList= await _unitOfWorkClientServices.companyClientServices.GetAllAsync("Company/getAllCompany");
            return View(productsVm);
        }
        public async Task<IActionResult> Product()
        {
            return View();
        }
        public async Task<IActionResult> CheckOut()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
