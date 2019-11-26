using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClydesTakeoutPrototype.Models;
using ClydesTakeoutPrototype.Models.SystemModels;
using ClydesTakeoutPrototype.Data;

namespace ClydesTakeoutPrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly DataContext _context;
        private readonly ILocalDataContext _context;

        public HomeController(ILogger<HomeController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
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
        public IActionResult Login([Bind("Email,Password,ID")] User currentUser)
        {
            if (ModelState.IsValid)
            {
                var dbUser = (from user in _context.UserDB
                              where (user.Email == currentUser.Email)
                              select user).FirstOrDefault();
                if (/*dbUser != null &&*/ dbUser?.Password == currentUser.Password)
                {
                    return RedirectToAction("Index", "Menus");
                }
            }
            return View(currentUser);
        }

        [HttpPost]
        public IActionResult SignUp([Bind("FirstName,LastName,Email,Password")] User newUser)
        {
            if (ModelState.IsValid)
            {
                var dbUser = (from user in _context.UserDB
                             where (user.Email == newUser.Email)
                             select user).FirstOrDefault();
                if (dbUser == null)
                {
                    _context.UserDB.Add(newUser);
                    _context.SaveDatabase(_context.UserDB);
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            return View(newUser);
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
