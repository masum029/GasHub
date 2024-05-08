using GasHub.Models;
using GasHub.Models.ViewModels;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class DeliveryAddressController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public DeliveryAddressController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetDeliveryAddressList()
        {
            var DeliveryAddress = await _unitOfWorkClientServices.deliveryAddressClientServices.GetAllAsync("DeliveryAddress/getAllDeliveryAddress");
            return Json(new { data = DeliveryAddress });
        }
        [HttpPost]
        public async Task<IActionResult> Create(DeliveryAddress model)
        {
            model.CreatedBy = "";
            var result = await _unitOfWorkClientServices.deliveryAddressClientServices.AddAsync(model, "DeliveryAddress/CreateDeliveryAddress");
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _unitOfWorkClientServices.deliveryAddressClientServices.GetByIdAsync(id, "DeliveryAddress/getDeliveryAddress");
            return Json(customer);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, DeliveryAddress model)
        {
            model.UpdatedBy = "User";
            var result = await _unitOfWorkClientServices.deliveryAddressClientServices.UpdateAsync(id, model, "DeliveryAddress/UpdateDeliveryAddress");
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.deliveryAddressClientServices.DeleteAsync(id, "DeliveryAddress/DeleteDeliveryAddress");
            return Json(deleted);
        }

    }
}
