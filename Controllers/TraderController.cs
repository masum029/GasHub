using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class TraderController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public TraderController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.traderClientServices.GetAllAsync("Trader/getAllTrader");
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Trader model)
        {
            bool result = await _unitOfWorkClientServices.traderClientServices.AddAsync(model, "Trader/CreateTrader");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.traderClientServices.GetByIdAsync(id, "Trader/getTrader");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _unitOfWorkClientServices.traderClientServices.GetByIdAsync(id, "Trader/getTrader");
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Trader model)
        {
            await _unitOfWorkClientServices.traderClientServices.UpdateAsync(id, model, "Trader/UpdateTrader");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.traderClientServices.GetByIdAsync(id, "Trader/getTrader");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.traderClientServices.DeleteAsync(id, "Trader/DeleteTrader");
            return RedirectToAction("Index");
        }
    }
}
