using Aktie_WebsiteMVCV2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aktie_WebsiteMVCV2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
