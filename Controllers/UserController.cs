using GasHub.Models;
using GasHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IApiClientServices _apiClientServices;
        public UserController(HttpClient httpClient, IApiClientServices apiClientServices)
        {
                _apiClientServices = apiClientServices;
            _httpClient = httpClient;

        }
        public async Task<IActionResult> Index()
        {
            List<User> users = await _apiClientServices.GetAsyns<List<User>>("User/getAllUser");
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] User user)
        {
            try
            {
                if (user == null)
                {
                    
                }
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
