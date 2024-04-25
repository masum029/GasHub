using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class RetailerController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public RetailerController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.retailerClientServices.GetAllAsync("Retailer/getAllRetailer");
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Retailer model)
        {
            bool result = await _unitOfWorkClientServices.retailerClientServices.AddAsync(model, "Retailer/CreateRetailer");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.retailerClientServices.GetByIdAsync(id, "Retailer/getRetailer");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _unitOfWorkClientServices.retailerClientServices.GetByIdAsync(id, "Retailer/getRetailer");
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Retailer model)
        {
            await _unitOfWorkClientServices.retailerClientServices.UpdateAsync(id, model, "Retailer/UpdateRetailer");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.retailerClientServices.GetByIdAsync(id, "Retailer/getRetailer");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.retailerClientServices.DeleteAsync(id, "Retailer/DeleteRetailer");
            return RedirectToAction("Index");
        }
    }
}
