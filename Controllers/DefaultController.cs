using Microsoft.AspNetCore.Mvc;

namespace GasHub.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
