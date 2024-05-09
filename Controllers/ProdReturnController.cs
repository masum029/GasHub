using GasHub.Models.ViewModels;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class ProdReturnController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public ProdReturnController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.prodReturnClientServices.GetAllAsync("ProdReturn/getAllProdReturn");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetallProdReturn()
        {
            var ProdReturn = await _unitOfWorkClientServices.prodReturnClientServices.GetAllAsync("ProdReturn/getAllProdReturn");
            return Json(new { data = ProdReturn });
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productList = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            var valveList = await _unitOfWorkClientServices.valveClientServices.GetAllAsync("Valve/getAllValve");
            var productSizeList = await _unitOfWorkClientServices.productSizeClientServices.GetAllAsync("ProductSize/getAllProductSize");
            var productReturnViewModel = new ProductReturnViewModel(productList, valveList, productSizeList);
            return View(productReturnViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductReturnViewModel model)
        {
            bool result = await _unitOfWorkClientServices.prodReturnClientServices.AddAsync(model.ProdReturn, "ProdReturn/CreateProdReturn");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.prodReturnClientServices.GetByIdAsync(id, "ProdReturn/getProdReturn");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productList = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            var valveList = await _unitOfWorkClientServices.valveClientServices.GetAllAsync("Valve/getAllValve");
            var productSizeList = await _unitOfWorkClientServices.productSizeClientServices.GetAllAsync("ProductSize/getAllProductSize");
            var productReturnList = await _unitOfWorkClientServices.prodReturnClientServices.GetByIdAsync(id, "ProdReturn/getProdReturn");
            var productReturnViewModel = new ProductReturnViewModel(productList, valveList, productSizeList);
            productReturnViewModel.ProdReturn = productReturnList;
            return View(productReturnViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductReturnViewModel model)
        {
            await _unitOfWorkClientServices.prodReturnClientServices.UpdateAsync(id, model.ProdReturn, "ProdReturn/UpdateProdReturn");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.prodReturnClientServices.GetByIdAsync(id, "ProdReturn/getProdReturn");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.prodReturnClientServices.DeleteAsync(id, "ProdReturn/DeleteProdReturn");
            return RedirectToAction("Index");
        }
    }
}
