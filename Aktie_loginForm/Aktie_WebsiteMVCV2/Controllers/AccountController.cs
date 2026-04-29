using Aktie_WebsiteMVCV2.DTO.Stock;
using Aktie_WebsiteMVCV2.Models;
using Aktie_WebsiteMVCV2.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Aktie_WebsiteMVCV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthApiService _authService;
        private readonly StockApiService _stockService;

        public AccountController(
            AuthApiService authService,
            StockApiService stockService)
        {
            _authService = authService;
            _stockService = stockService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AktieView(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                return View();

            var stock = await _stockService.GetStock(symbol);

            if (stock == null)
            {
                ViewBag.Error = "Aktie ikke fundet";
                return View();
            }

            return View(stock);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = await _authService.Login(model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Navn),
                    new Claim("KundeId", result.KundeId.ToString())
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                return RedirectToAction("AktieView");
            }

            ViewBag.ErrorMessage = "Forkert email eller password";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _authService.Register(model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            model.ErrorMessage = "Bruger kunne ikke oprettes";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}