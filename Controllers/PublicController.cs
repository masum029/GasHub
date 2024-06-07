using GasHub.Models;
using GasHub.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GasHub.Controllers
{
    public class PublicController : Controller
    {
        private readonly IClientServices<Register> _registerServices;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authenticationService;

        public PublicController(IClientServices<Register> registerServices,
            ITokenService tokenService,
            IAuthService authenticationService
            )
        {
            _registerServices = registerServices;
            _tokenService = tokenService;
            _authenticationService = authenticationService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(Register model)
        {
            model.Roles = ["User"];
            var register = await _registerServices.PostClientAsync( "User/Create" , model);
            if (register.Success)
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(Login model, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = ReturnUrl;
                return View("Login", model);
            }

            var loginResponse = await _authenticationService.Login(model);

            if (loginResponse.token == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                ViewData["ReturnUrl"] = ReturnUrl;
                return View("Login", model);
            }

            _tokenService.SaveToken(loginResponse.token);
            await UserLogin(loginResponse.token);

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _tokenService.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task UserLogin(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var claim in jwt.Claims)
            {
                identity.AddClaim(claim);
            }

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Optionally extract and log user details
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var userName = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var roles = jwt.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        }

    }
}
