using GasHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class DefaultController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
        }
        public async  Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("GasHubClient");
            var response = await client.GetAsync("Company/getAllCompany");
            return View();
        }

        public async Task<IActionResult> GetAllCOmpany()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("GasHubClient");
                var response = await client.GetAsync("endpoint");
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Index2()
        {
            var response = await _httpClient.GetAsync("/api/weather");
            if (response.IsSuccessStatusCode)
            {
                //var weatherData = await response.Content.readasa
                return View();
            }

            // Handle error case
            return View("Error");
        }
    }
}
