using GasHub.Models;
using GasHub.Models.ViewModels;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class StockController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public StockController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var result = await _unitOfWorkClientServices.stockClientServices.GetAllAsync("Stock/getAllStock");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productList = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            var traderList = await _unitOfWorkClientServices.traderClientServices.GetAllAsync("Trader/getAllTrader");
            var StockViewModel = new StockViewModel(productList, traderList);
            return View(StockViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StockViewModel model)
        {
            bool result = await _unitOfWorkClientServices.stockClientServices.AddAsync(model.Stock, "Stock/CreateStock");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.stockClientServices.GetByIdAsync(id, "Stock/getStock");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var result = await _unitOfWorkClientServices.stockClientServices.GetByIdAsync(id, "Stock/getStock");
            var productList = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            var traderList = await _unitOfWorkClientServices.traderClientServices.GetAllAsync("Trader/getAllTrader");
            var StockViewModel = new StockViewModel(productList, traderList);
            StockViewModel.Stock = result;
            return View(StockViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, StockViewModel model)
        {
            await _unitOfWorkClientServices.stockClientServices.UpdateAsync(id, model.Stock, "Stock/UpdateStock");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.stockClientServices.GetByIdAsync(id, "Stock/getStock");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.stockClientServices.DeleteAsync(id, "Stock/DeleteStock");
            return RedirectToAction("Index");
        }
    }
}
