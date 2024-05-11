using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class ValveController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public ValveController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetallValve()
        {
            var Valve = await _unitOfWorkClientServices.valveClientServices.GetAllAsync("Valve/getAllValve");
            return Json(new { data = Valve });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Valve model)
        {
            model.CreatedBy = "mamun";
            var Valve = await _unitOfWorkClientServices.valveClientServices.AddAsync(model, "Valve/CreateValve");
            return Json(Valve);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var Valve = await _unitOfWorkClientServices.valveClientServices.GetByIdAsync(id, "Valve/getValve");
            return Json(Valve);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, Valve model)
        {
            model.UpdatedBy = "mamun";
            var Valve = await _unitOfWorkClientServices.valveClientServices.UpdateAsync(id, model, "Valve/UpdateValve");
            return Json(Valve);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.valveClientServices.DeleteAsync(id, "Valve/DeleteValve");
            return Json(deleted);
        }
    }
}
