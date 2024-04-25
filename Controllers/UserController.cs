using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public UserController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkClientServices.userClientServices.GetAllAsync("User/getAllUser");
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            bool result = await _unitOfWorkClientServices.userClientServices.AddAsync(model, "User/CreateUser");
            return result ? RedirectToAction("Index") : View(default);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _unitOfWorkClientServices.userClientServices.GetByIdAsync(id, "User/getUser");
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _unitOfWorkClientServices.userClientServices.GetByIdAsync(id, "User/getUser");
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, User model)
        {
            await _unitOfWorkClientServices.userClientServices.UpdateAsync(id, model, "User/UpdateUser");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWorkClientServices.userClientServices.GetByIdAsync(id, "User/getUser");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSuccessfull(Guid id)
        {
            await _unitOfWorkClientServices.userClientServices.DeleteAsync(id, "User/DeleteUser");
            return RedirectToAction("Index");
        }
    }
}
