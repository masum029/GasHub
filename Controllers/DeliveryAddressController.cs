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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var deliveryAddress = await _unitOfWorkClientServices.deliveryAddressClientServices.GetAllAsync("DeliveryAddress/getAllDeliveryAddress");
            return View(deliveryAddress);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _unitOfWorkClientServices.userClientServices.GetAllAsync("User/getAllUser");
            var deliveryAddressViewModel = new DeliveryAddressViewModel(users);
            return View(deliveryAddressViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(DeliveryAddressViewModel model)
        {
            bool result = await _unitOfWorkClientServices.deliveryAddressClientServices.AddAsync(model.DeliveryAddress, "DeliveryAddress/CreateDeliveryAddress");
            return result ? RedirectToAction("Index") : RedirectToAction("Error");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.deliveryAddressClientServices.GetByIdAsync(id, "DeliveryAddress/getDeliveryAddress");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var users = await _unitOfWorkClientServices.userClientServices.GetAllAsync("User/getAllUser");
            var deliveryAddress = await _unitOfWorkClientServices.deliveryAddressClientServices.GetByIdAsync(id, "DeliveryAddress/getDeliveryAddress");
            var deliveryAddressViewModel = new DeliveryAddressViewModel(users);
            deliveryAddressViewModel.DeliveryAddress = deliveryAddress;
            return View(deliveryAddressViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, DeliveryAddressViewModel model)
        {
            await _unitOfWorkClientServices.deliveryAddressClientServices.UpdateAsync(id, model.DeliveryAddress, "DeliveryAddress/UpdateDeliveryAddress");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _unitOfWorkClientServices.deliveryAddressClientServices.GetByIdAsync(id, "DeliveryAddress/getDeliveryAddress");
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.deliveryAddressClientServices.DeleteAsync(id, "DeliveryAddress/DeleteDeliveryAddress");
            return RedirectToAction("Index");
        }
    }
}
