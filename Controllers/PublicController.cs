using GasHub.Models;
using GasHub.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GasHub.Controllers
{
    public class PublicController : Controller
    {
        private readonly IUnitOfWorkClientServices _unitOfWorkClientServices;
        private readonly ITokenService _tokenService;


        public PublicController(IUnitOfWorkClientServices unitOfWorkClientServices, ITokenService tokenService)
        {
            _unitOfWorkClientServices = unitOfWorkClientServices;
            _tokenService = tokenService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(Register model)
        {
            model.Roles = ["User"];
            var register = await _unitOfWorkClientServices.registerUserClientServices.AddAsync(model, "User/Create");
            return Json(register);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(Login model)
        {
            var loginResponse = await _unitOfWorkClientServices.loginUserClientServices.LoginAsync(model, "Auth/Login");
            if (loginResponse.token == null)
            {
                return View(loginResponse);
            }

            var tokenConfiger = new TokenConfig { Token = loginResponse.token };
            _tokenService.SaveToken(loginResponse.token);
            await UserLogin(tokenConfiger);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _tokenService.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task<UserDetails> UserLogin(TokenConfig tokenConfig)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(tokenConfig.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var claim in jwt.Claims)
            {
                identity.AddClaim(claim);
            }

            var principal = new ClaimsPrincipal(identity);

            // Set the user principal in HttpContext
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Extracting user details from claims
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var userName = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var roles = jwt.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            return new UserDetails
            {
                UserId = userId,
                UserName = userName,
                Roles = roles
            };
        }

    }
}
