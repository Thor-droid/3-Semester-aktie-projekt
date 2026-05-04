using Aktie_WebsiteMVCV2.BusinessLogicLayer;
using Aktie_WebsiteMVCV2.Models;
using Aktie_WebsiteMVCV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aktie_WebsiteMVCV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountLogic _accountLogic;
        private readonly StockApiService _stockService;
        private readonly ILoginService _loginService;

        public AccountController(
            AccountLogic accountLogic,
            StockApiService stockService,
            ILoginService loginService)
        {
            _accountLogic = accountLogic;
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
            var result = await _accountLogic.Login(model);

            if (result != null)
            {
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

            var success = await _accountLogic.Register(model);

            if (success)
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