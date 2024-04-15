using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public CompanyController(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var companys = await _unitOfWorkClientServices.companyClientServices.GetAllAsync("Company/getAllCompany");
            return View(companys);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Company customer)
        {
            bool result = await _unitOfWorkClientServices.companyClientServices.AddAsync(customer, "Company/Create");
            return result ? RedirectToAction("Index") : RedirectToAction("Error");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var customer = await _unitOfWorkClientServices.companyClientServices.GetByIdAsync(id, "Company/getCompany");
            return View(customer);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var customer = await _unitOfWorkClientServices.companyClientServices.GetByIdAsync(id, "Company/getCompany");
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Company company)
        {
            await _unitOfWorkClientServices.companyClientServices.UpdateAsync(id, company, "Company/UpdateCompany");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _unitOfWorkClientServices.companyClientServices.DeleteAsync(id, "Company/DeleteCompany");
            return RedirectToAction("Index");
        }
    }
}
