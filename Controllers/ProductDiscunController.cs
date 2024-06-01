using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class ProductDiscunController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public ProductDiscunController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetallProductDiscun()
        {
            var ProductDiscun = await _unitOfWorkClientServices.productDiscunClientServices.GetAllAsync("ProductDiscunt/getAllProductDiscunt");
            return Json(new { data = ProductDiscun });
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDiscunt model)
        {
            model.CreatedBy = "mamun";
            var ProductDiscun = await _unitOfWorkClientServices.productDiscunClientServices.AddAsync(model, "ProductDiscunt/CreateProductDiscunt");
            return Json(ProductDiscun);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ProductDiscunt = await _unitOfWorkClientServices.productDiscunClientServices.GetByIdAsync(id, "ProductDiscunt/getProductDiscunt");
            return Json(ProductDiscunt);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, ProductDiscunt model)
        {
            model.UpdatedBy = "mamun";
            var ProductDiscunt = await _unitOfWorkClientServices.productDiscunClientServices.UpdateAsync(id, model, "ProductDiscunt/UpdateProductDiscunt");
            return Json(ProductDiscunt);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.productDiscunClientServices.DeleteAsync(id, "ProductDiscunt/DeleteProductDiscunt");
            return Json(deleted);
        }
    }
}
