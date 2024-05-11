using GasHub.Models;
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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetallStock()
        {
            var stock = await _unitOfWorkClientServices.stockClientServices.GetAllAsync("Stock/getAllStock");
            return Json(new { data = stock });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Stock model)
        {
            model.CreatedBy = "mamun";
            var stock = await _unitOfWorkClientServices.stockClientServices.AddAsync(model, "Stock/CreateStock");
            return Json(stock);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var stock = await _unitOfWorkClientServices.stockClientServices.GetByIdAsync(id, "Stock/getStock");
            return Json(stock);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, Stock model)
        {
            model.UpdatedBy = "mamun";
            var stock = await _unitOfWorkClientServices.stockClientServices.UpdateAsync(id, model, "Stock/UpdateStock");
            return Json(stock);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.stockClientServices.DeleteAsync(id, "Stock/DeleteStock");
            return Json(deleted);
        }
    }
}
