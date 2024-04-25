using GasHub.Models;
using GasHub.Models.ViewModels;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public OrderController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.orderClientServices.GetAllAsync("Order/getAllOrder");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _unitOfWorkClientServices.userClientServices.GetAllAsync("User/getAllUser");
            var products = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            var orderView = new OrderViewModel(users,products);
            return View(orderView);
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            bool result = await _unitOfWorkClientServices.orderClientServices.AddAsync(model.Order, "Order/CreateOrder");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.orderClientServices.GetByIdAsync(id, "Order/getOrder");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var order = await _unitOfWorkClientServices.orderClientServices.GetByIdAsync(id, "Order/getOrder");
            var users = await _unitOfWorkClientServices.userClientServices.GetAllAsync("User/getAllUser");
            var products = await _unitOfWorkClientServices.productClientServices.GetAllAsync("Product/getAllProduct");
            var orderView = new OrderViewModel(users, products);
            orderView.Order = order;
            return View(orderView);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, OrderViewModel model)
        {
            await _unitOfWorkClientServices.orderClientServices.UpdateAsync(id, model.Order, "Order/UpdateOrder");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.orderClientServices.GetByIdAsync(id, "Order/getOrder");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.orderClientServices.DeleteAsync(id, "Order/DeleteOrder");
            return RedirectToAction("Index");
        }
    }
}
