using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class ProductSizeController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public ProductSizeController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.productSizeClientServices.GetAllAsync("ProductSize/getAllProductSize");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetallProductSize()
        {
            var productSize = await _unitOfWorkClientServices.productSizeClientServices.GetAllAsync("ProductSize/getAllProductSize");
            return Json(new { data = productSize });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductSize model)
        {
            bool result = await _unitOfWorkClientServices.productSizeClientServices.AddAsync(model, "ProductSize/CreateProductSize");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.productSizeClientServices.GetByIdAsync(id, "ProductSize/getProductSize");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _unitOfWorkClientServices.productSizeClientServices.GetByIdAsync(id, "ProductSize/getProductSize");
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductSize model)
        {
            await _unitOfWorkClientServices.productSizeClientServices.UpdateAsync(id, model, "ProductSize/UpdateProductSize");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.productSizeClientServices.GetByIdAsync(id, "ProductSize/getProductSize");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.productSizeClientServices.DeleteAsync(id, "ProductSize/DeleteProductSize");
            return RedirectToAction("Index");
        }
    }
}
