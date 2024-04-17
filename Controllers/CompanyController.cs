using GasHub.Models;
using GasHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GasHub.Controllers
{
    public class CompanyController : Controller
    {
        private readonly HttpClient _httpClient;
        
        private readonly IApiClientServices _apiClientService;


        public CompanyController(HttpClient httpClient, IApiClientServices apiClientService)
        {
            _httpClient = httpClient;            
            _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));
            
        }
        public async Task <IActionResult> Index()
        {
            //string responseBody = await _apiClientService.GetResponseFromApi("Company/getAllCompany", "");
            List<Company> companies = await _apiClientService.GetAsyns<List<Company>>("Company/getAllCompany");
            return View(companies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {           

            return View();
        }

        [HttpPost]
        [Route("Company/Create")]
        public async Task<IActionResult> Create([FromForm] Company company)
        {
            try
            {
                // Specify the type argument explicitly as Company
                Company success = await _apiClientService.PostAsync<Company>("Company/Create", company);

                // Optionally, you can check if the operation was successful
                if (true)
                {
                    // Handle success
                }
                else
                {
                    // Handle failure
                }
                return View();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw;
            }
         
        }

    }
}
