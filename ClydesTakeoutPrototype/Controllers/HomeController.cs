using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClydesTakeoutPrototype.Models;
using ClydesTakeoutPrototype.Models.SystemModels;

namespace ClydesTakeoutPrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        public IActionResult SignUp()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Login([Bind("Email,Password,ID")] User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult SignUp([Bind("FirstName,LastName,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
