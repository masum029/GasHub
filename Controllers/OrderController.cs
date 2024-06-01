using GasHub.Models;
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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderList()
        {
            var orders = await _unitOfWorkClientServices.orderClientServices.GetAllAsync("Order/getAllOrder");
            return Json(new { data = orders });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order model)
        {
            model.CreatedBy = "mamun";
            var orders = await _unitOfWorkClientServices.orderClientServices.AddAsync(model, "Order/CreateOrder");
            return Json(orders);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _unitOfWorkClientServices.orderClientServices.GetByIdAsync(id, "Order/getOrder");
            return Json(order);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, Order model)
        {
            model.UpdatedBy = "mamun";
            var order = await _unitOfWorkClientServices.orderClientServices.UpdateAsync(id, model, "Order/UpdateOrder");
            return Json(order);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.orderClientServices.DeleteAsync(id, "Order/DeleteOrder");
            return Json(deleted);
        }
    }
}
