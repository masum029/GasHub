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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            return Json(new { data = products });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            model.CreatedBy = "mamun";
            var product = await _unitOfWorkClientServices.productClientServices.AddAsync(model, "Product/CreateProduct");
            return Json(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _unitOfWorkClientServices.productClientServices.GetByIdAsync(id, "Product/getProduct");
            return Json(product);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, Product model)
        {
            model.UpdatedBy = "mamun";
            var product = await _unitOfWorkClientServices.productClientServices.UpdateAsync(id, model, "Product/UpdateProduct");
            return Json(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.productClientServices.DeleteAsync(id, "Product/DeleteProduct");
            return Json(deleted);
        }
    }
}
