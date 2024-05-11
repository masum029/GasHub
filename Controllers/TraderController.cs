using GasHub.Models;
using GasHub.Models.ViewModels;
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
        public async Task<IActionResult> GetallTrader()
        {
            var traders = await _unitOfWorkClientServices.traderClientServices.GetAllAsync("Trader/getAllTrader");
            return Json(new { data = traders });
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.traderClientServices.GetAllAsync("Trader/getAllTrader");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companyList = await _unitOfWorkClientServices.companyClientServices.GetAllAsync("Company/getAllCompany");
            var TraderViewMode = new TraderViewModel(companyList);
            return View(TraderViewMode);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TraderViewModel model)
        {
            bool result = await _unitOfWorkClientServices.traderClientServices.AddAsync(model.Trader, "Trader/CreateTrader");
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
            var companyList = await _unitOfWorkClientServices.companyClientServices.GetAllAsync("Company/getAllCompany");
            var TraderViewMode = new TraderViewModel(companyList);
            TraderViewMode.Trader = result;
            return View(TraderViewMode);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, TraderViewModel model)
        {
            await _unitOfWorkClientServices.traderClientServices.UpdateAsync(id, model.Trader, "Trader/UpdateTrader");
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
