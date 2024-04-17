using GasHub.Models;
using GasHub.Services.Implementation;
using GasHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace GasHub.Controllers
{
    public class DefaultController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiClientServices _apiClientService;
        private readonly string _serverAddress = "localhost";
        

        public DefaultController(HttpClient httpClient, IHttpClientFactory httpClientFactory, IApiClientServices apiClientService)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
            _apiClientService = apiClientService ?? throw new ArgumentNullException(nameof(apiClientService));

            if (!IsServerReachable(_serverAddress))
            {
                throw new UnreachableException("Unable to reach API SERVER");
            }
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                string responseBody0 = await _apiClientService.GetAllCompanies();
                //string responseBody = await _apiClientService.GetResponseFromApi("Company/getAllCompany", "");

                return View();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> Indexwithhttpvalidate()
        {

            // Create an HttpClientHandler
            HttpClientHandler handler = new HttpClientHandler();

            // Configure the handler to accept all certificates
            handler.ServerCertificateCustomValidationCallback = ValidateCertificate;

            // Create HttpClient with the custom handler
            HttpClient client = new HttpClient(handler);

            // Make your API request using the HttpClient instance
            HttpResponseMessage response = await client.GetAsync("https://localhost:7128/api/Company/getAllCompany");

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                // Process successful response
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                // Handle error response
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
            }


            //var client = _httpClientFactory.CreateClient("GasHubClient");
            //var response = await client.GetAsync("Company/getAllCompany");
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

        private static bool ValidateCertificate(HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Accept all certificates, even if there are errors
            return true;
        }

        //using System.Net.NetworkInformation;

        public bool IsServerReachable(string serverAddress)
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(serverAddress);
                return reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
        }

    }
}
