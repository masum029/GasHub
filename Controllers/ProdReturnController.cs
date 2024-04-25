using GasHub.Models;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProdReturn model)
        {
            bool result = await _unitOfWorkClientServices.prodReturnClientServices.AddAsync(model, "ProdReturn/CreateProdReturn");
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
            var result = await _unitOfWorkClientServices.prodReturnClientServices.GetByIdAsync(id, "ProdReturn/getProdReturn");
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProdReturn model)
        {
            await _unitOfWorkClientServices.prodReturnClientServices.UpdateAsync(id, model, "ProdReturn/UpdateProdReturn");
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
