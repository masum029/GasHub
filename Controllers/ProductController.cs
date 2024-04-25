using GasHub.Models;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            bool result = await _unitOfWorkClientServices.productClientServices.AddAsync(model, "Product/CreateProduct");
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
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Product model)
        {
            await _unitOfWorkClientServices.productClientServices.UpdateAsync(id, model, "Product/UpdateProduct");
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
