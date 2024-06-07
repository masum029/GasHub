using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public UserController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetallUser()
        {
            var users = await _unitOfWorkClientServices.userClientServices.GetAllAsync("User/GetAllUserDetails");
            return Json(new { data = users });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Register model)
        {
            var user = await _unitOfWorkClientServices.registerUserClientServices.AddAsync(model, "User/Create");
            return Json(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _unitOfWorkClientServices.userClientServices.GetByIdAsync(id, "User/GetUserDetails");
            return Json(user);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.traderClientServices.DeleteAsync(id, "User/Delete");
            return Json(deleted);
        }
    }
}
