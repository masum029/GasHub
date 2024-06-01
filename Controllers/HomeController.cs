using GasHub.Dtos;
using GasHub.Models;
using GasHub.Models.ViewModels;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GasHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;
        public HomeController(ILogger<HomeController> logger , IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _logger = logger;
            _unitOfWorkClientServices=unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productsVm = new ProductViewModel();
            var products = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            var companies = await _unitOfWorkClientServices.companyClientServices.GetAllAsync("Company/getAllCompany");
            var discounts = await _unitOfWorkClientServices.productDiscunClientServices.GetAllAsync("ProductDiscunt/getAllProductDiscunt");

            if (products != null)
            {
                productsVm.ProductList = products;
            }

            if (companies != null)
            {
                productsVm.companiList = companies;
            }

            if (discounts != null)
            {
                productsVm.productDiscunts = discounts;
            }

            return View(productsVm);
        }
        public async Task<IActionResult> Product()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CheckOut()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddAddress()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmOrder()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAddressToDb(AddressDtos model)
        {
            var deliveryAddress = new DeliveryAddress();
            deliveryAddress.UserId = model.UserId;
            deliveryAddress.CreatedBy = "";
            deliveryAddress.Phone = model.ContactNumber;
            deliveryAddress.Mobile = model.ContactNumber;
            deliveryAddress.Address = $"{model.Division} {model.District} {model.Subdistrict} {model.Area} {model.HouseHolding} {model.StreetAddress} {model.postCode}";
            //$"{model.Division} {model.District} {model.Subdistrict} {model.Area} {model.HouseHolding} {model.StreetAddress} {model.postCode}"


            if (!string.IsNullOrEmpty(deliveryAddress.Address))
            {
                var result = await _unitOfWorkClientServices.deliveryAddressClientServices.AddAsync(deliveryAddress, "DeliveryAddress/CreateDeliveryAddress");
                if (result)
                {
                    return RedirectToAction("CheckOut", "Home");
                }
            }


            return RedirectToAction("AddAddress");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
