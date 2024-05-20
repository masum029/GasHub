using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DefaultController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
