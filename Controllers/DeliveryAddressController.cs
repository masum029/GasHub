using GasHub.Models;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DeliveryAddress model)
        {
            bool result = await _unitOfWorkClientServices.deliveryAddressClientServices.AddAsync(model, "DeliveryAddress/Create");
            return result ? RedirectToAction("Index") : RedirectToAction("Error");
        }
    }
}
