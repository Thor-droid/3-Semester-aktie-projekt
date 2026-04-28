using Aktie_WebsiteMVCV2.DTO.Stock;
using Aktie_WebsiteMVCV2.Models;
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
        private string authApiUrl = "https://localhost:7120/api/auth";
        private string stockApiUrl = "https://localhost:7120/api/stock";

        // ---------------- AKTIEVIEW ----------------
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AktieView(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                return View();

            using var client = new HttpClient();

            var response = await client.GetAsync($"{stockApiUrl}/{symbol}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Aktie ikke fundet";
                return View();
            }

            var stock = await response.Content.ReadFromJsonAsync<GlobalQuoteDto>();

            return View(stock);
        }

        // ---------------- LOGIN ----------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            using var client = new HttpClient();

            var response = await client.PostAsJsonAsync($"{authApiUrl}/login", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, result.Navn),
            new Claim("KundeId", result.KundeId.ToString())
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal
                );

                return RedirectToAction("AktieView", "Account");
            }

            ViewBag.ErrorMessage = "Forkert email eller password";
            return View();
        }

        // ---------------- REGISTER ----------------
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

            using var client = new HttpClient();

            var response = await client.PostAsJsonAsync($"{authApiUrl}/register", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

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