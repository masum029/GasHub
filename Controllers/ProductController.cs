using GasHub.Models;
using GasHub.Models.ViewModels;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public ProductController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            return Json(new { data = products });
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companys = await _unitOfWorkClientServices.companyClientServices.GetAllAsync("Company/getAllCompany");
            var valv = await _unitOfWorkClientServices.valveClientServices.GetAllAsync("Valve/getAllValve");
            var productSize = await _unitOfWorkClientServices.productSizeClientServices.GetAllAsync("ProductSize/getAllProductSize");
            var ProductViewModel = new ProductViewModel(companys,valv,productSize);
            return View(ProductViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            bool result = await _unitOfWorkClientServices.productClientServices.AddAsync(model.Product, "Product/CreateProduct");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.productClientServices.GetByIdAsync(id, "Product/getProduct");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _unitOfWorkClientServices.productClientServices.GetByIdAsync(id, "Product/getProduct");
            var companys = await _unitOfWorkClientServices.companyClientServices.GetAllAsync("Company/getAllCompany");
            var valv = await _unitOfWorkClientServices.valveClientServices.GetAllAsync("Valve/getAllValve");
            var productSize = await _unitOfWorkClientServices.productSizeClientServices.GetAllAsync("ProductSize/getAllProductSize");
            var ProductViewModel = new ProductViewModel(companys, valv, productSize);
            ProductViewModel.Product = result;
            return View(ProductViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel model)
        {
            await _unitOfWorkClientServices.productClientServices.UpdateAsync(id, model.Product, "Product/UpdateProduct");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.productClientServices.GetByIdAsync(id, "Product/getProduct");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.productClientServices.DeleteAsync(id, "Product/DeleteProduct");
            return RedirectToAction("Index");
        }
    }
}
