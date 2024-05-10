using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class ProdReturnController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public ProdReturnController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetallProdReturn()
        {
            var ProdReturn = await _unitOfWorkClientServices.prodReturnClientServices.GetAllAsync("ProdReturn/getAllProdReturn");
            return Json(new { data = ProdReturn });
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProdReturn model)
        {
            model.CreatedBy = "mamun";
            var ProdReturn = await _unitOfWorkClientServices.prodReturnClientServices.AddAsync(model, "ProdReturn/CreateProdReturn");
            return Json(ProdReturn);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ProdReturn = await _unitOfWorkClientServices.prodReturnClientServices.GetByIdAsync(id, "ProdReturn/getProdReturn");
            return Json(ProdReturn);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, ProdReturn model)
        {
            model.UpdatedBy = "mamun";
            var productReturn = await _unitOfWorkClientServices.prodReturnClientServices.UpdateAsync(id, model, "ProdReturn/UpdateProdReturn");
            return Json(productReturn);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.prodReturnClientServices.DeleteAsync(id, "ProdReturn/DeleteProdReturn");
            return Json(deleted);
        }
    }
}
