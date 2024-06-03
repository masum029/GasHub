using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class RoleController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public RoleController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetallRole()
        {
            var roles = await _unitOfWorkClientServices.roleClientServices.GetAllAsync("Role/GetAll");
            return Json(new { data = roles });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Role model)
        {
            model.CreatedBy = "mamun";
            var role = await _unitOfWorkClientServices.roleClientServices.AddAsync(model, "Role/Create");
            return Json(role);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _unitOfWorkClientServices.roleClientServices.GetByIdAsync(id, "Role");
            return Json(role);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, Role model)
        {
            model.UpdatedBy = "mamun";
            var role = await _unitOfWorkClientServices.roleClientServices.UpdateAsync(id, model, "Role/Edit");
            return Json(role);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.roleClientServices.DeleteAsync(id, "Role/Delete");
            return Json(deleted);
        }
    }
}
