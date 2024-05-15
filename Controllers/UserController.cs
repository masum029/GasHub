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
        
    }
}
