using Aktie_WebsiteMVCV2.DTO.Stock;
using Aktie_WebsiteMVCV2.Models;
using Aktie_WebsiteMVCV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Aktie_WebsiteMVCV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthApiService _authService;
        private readonly StockApiService _stockService;
        private readonly ILoginService _loginService;

        public AccountController(
            AuthApiService authService,
            StockApiService stockService,
            ILoginService loginService)
        {
            _authService = authService;
            _stockService = stockService;
            _loginService = loginService;
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

                await _loginService.SignInUser(HttpContext, result);

                return RedirectToAction("AktieView");
            }

            ViewBag.ErrorMessage = "Forkert email eller password";
            return View(model);
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
            await _loginService.SignOutUser(HttpContext);

            return RedirectToAction("Login");
        }
    }
}