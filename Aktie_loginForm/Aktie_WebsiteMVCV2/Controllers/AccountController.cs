using Aktie_WebsiteMVCV2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Aktie_WebsiteMVCV2.Controllers
{
    public class AccountController : Controller
    {
        private string apiUrl = "https://localhost:7120/api/auth";

        // ---------------- AKTIEVIEW ----------------
        [HttpGet]
        [Authorize]
        public IActionResult AktieView()
        {
            return View();
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

            var response = await client.PostAsJsonAsync($"{apiUrl}/login", model);

            if (response.IsSuccessStatusCode)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Email)
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

            var response = await client.PostAsJsonAsync($"{apiUrl}/register", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            model.ErrorMessage = "Bruger kunne ikke oprettes";
            return View(model);
        }
    }
}