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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetallProductSize()
        {
            var productSize = await _unitOfWorkClientServices.productSizeClientServices.GetAllAsync("ProductSize/getAllProductSize");
            return Json(new { data = productSize });
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductSize model)
        {
            model.CreatedBy = "mamun";
            var productSize = await _unitOfWorkClientServices.productSizeClientServices.AddAsync(model, "ProductSize/CreateProductSize");
            return Json(productSize);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productSize = await _unitOfWorkClientServices.productSizeClientServices.GetByIdAsync(id, "ProductSize/getProductSize");
            return Json(productSize);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, ProductSize model)
        {
            model.UpdatedBy = "mamun";
            var productSize = await _unitOfWorkClientServices.productSizeClientServices.UpdateAsync(id, model, "ProductSize/UpdateProductSize");
            return Json(productSize);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.productSizeClientServices.DeleteAsync(id, "ProductSize/DeleteProductSize");
            return Json(deleted);
        }
    }
}
