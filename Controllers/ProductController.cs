using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;
        private readonly IFileUploader _fileUploder;

        public ProductController(IUnitOfWorkClientServices unitOfWorkClientServices, IFileUploader fileUploder)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
            _fileUploder = fileUploder;
        }
        public async Task<IActionResult> Index()
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
            var prdI = await _fileUploder.ImgUploader(model.FormFile);
            model.CreatedBy = "mamun";
            model.ProdImage = prdI;
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
            // Get the product by ID
            var product = await _unitOfWorkClientServices.productClientServices.GetByIdAsync(id, "Product/getProduct");

            if (product.ProdImage == null)
            {
                return NotFound(new { Message = "Product not found" });
            }
            //Delete the product image
            bool deleteImg = await _fileUploder.DeleteFile(product.ProdImage);

            if (!deleteImg)
            {
                return StatusCode(500, new { Message = "Error deleting product image" });
            }

            // Delete the product
            var deleted = await _unitOfWorkClientServices.productClientServices.DeleteAsync(id, "Product/DeleteProduct");

            if (!deleted)
            {
                return StatusCode(500, new { Message = "Error deleting product" });
            }

            return Json(deleted);

        }
    }
}
