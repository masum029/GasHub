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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetallTrader()
        {
            var traders = await _unitOfWorkClientServices.traderClientServices.GetAllAsync("Trader/getAllTrader");
            return Json(new { data = traders });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Trader model)
        {
            model.CreatedBy = "mamun";
            model.BIN = "Tast Bin";
            var trader = await _unitOfWorkClientServices.traderClientServices.AddAsync(model, "Trader/CreateTrader");
            return Json(trader);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var trader = await _unitOfWorkClientServices.traderClientServices.GetByIdAsync(id, "Trader/getTrader");
            return Json(trader);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, Trader model)
        {
            model.UpdatedBy = "mamun";
            model.BIN = "Tast Bin";
            var trader = await _unitOfWorkClientServices.traderClientServices.UpdateAsync(id, model, "Trader/UpdateTrader");
            return Json(trader);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.traderClientServices.DeleteAsync(id, "Trader/DeleteTrader");
            return Json(deleted);
        }
    }
}
