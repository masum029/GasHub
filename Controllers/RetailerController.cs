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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetallRetailer()
        {
            var retailer = await _unitOfWorkClientServices.retailerClientServices.GetAllAsync("Retailer/getAllRetailer");
            return Json(new { data = retailer });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Retailer model)
        {
            model.CreatedBy = "mamun";
            var retailer = await _unitOfWorkClientServices.retailerClientServices.AddAsync(model, "Retailer/CreateRetailer");
            return Json(retailer);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var retailer = await _unitOfWorkClientServices.retailerClientServices.GetByIdAsync(id, "Retailer/getRetailer");
            return Json(retailer);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, Retailer model)
        {
            model.UpdatedBy = "mamun";
            var productSize = await _unitOfWorkClientServices.retailerClientServices.UpdateAsync(id, model, "Retailer/UpdateRetailer");
            return Json(productSize);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.retailerClientServices.DeleteAsync(id, "Retailer/DeleteRetailer");
            return Json(deleted);
        }
        
    }
}
