﻿using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Rnd
{
    public class Company1Controller : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;

        public Company1Controller(IUnitOfWorkClientServices unitOfWorkClientServices)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanyList()
        {
            var companys = await _unitOfWorkClientServices.companyClientServices.GetAllAsync("Company/getAllCompany");
            return Json(new { data = companys });
        }
        [HttpPost]
        public async Task<IActionResult> Create( Company company)
        {
            bool result = await _unitOfWorkClientServices.companyClientServices.AddAsync(company, "Company/CreateCompany");
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var customer = await _unitOfWorkClientServices.companyClientServices.GetByIdAsync(id, "Company/getCompany");
            return Json(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Guid id,  Company company)
        {
            var result =  await _unitOfWorkClientServices.companyClientServices.UpdateAsync(id, company, "Company/UpdateCompany");
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWorkClientServices.companyClientServices.DeleteAsync(id, "Company/DeleteCompany");
            return Json(deleted);
        }

    }
}
