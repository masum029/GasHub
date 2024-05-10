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
        [HttpGet]
        public async Task<IActionResult> GetallValve()
        {
            var Valve = await _unitOfWorkClientServices.valveClientServices.GetAllAsync("Valve/getAllValve");
            return Json(new { data = Valve });
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.valveClientServices.GetAllAsync("Valve/getAllValve");
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Valve model)
        {
            bool result = await _unitOfWorkClientServices.valveClientServices.AddAsync(model, "Valve/CreateValve");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.valveClientServices.GetByIdAsync(id, "Valve/getValve");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _unitOfWorkClientServices.valveClientServices.GetByIdAsync(id, "Valve/getValve");
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Valve model)
        {
            await _unitOfWorkClientServices.valveClientServices.UpdateAsync(id, model, "Valve/UpdateValve");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.valveClientServices.GetByIdAsync(id, "Valve/getValve");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.valveClientServices.DeleteAsync(id, "Valve/DeleteValve");
            return RedirectToAction("Index");
        }
    }
}
