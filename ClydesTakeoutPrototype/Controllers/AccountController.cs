using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClydesTakeoutPrototype.Data;
using ClydesTakeoutPrototype.Models.SystemModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ClydesTakeoutPrototype.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ILocalDataContext _context;

        public AccountController(ILogger<AccountController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password,ID")] User currentUser)
        {
            if (ModelState.IsValid)
            {
                var dbUser = (from user in _context.UserDB
                              where (user.Email == currentUser.Email)
                              select user).FirstOrDefault();
                if (dbUser?.Password == currentUser.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, dbUser.ID.ToString()),
                        new Claim(ClaimTypes.Role, dbUser.UserPermissions.ToString())
                    };

                    await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "login")));
                    
                    return RedirectToAction("Index", "Menus");
                }
            }
            return View(currentUser);
        }

        public IActionResult SignUp()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([Bind("FirstName,LastName,Email,Password")] User newUser)
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

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, newUser.ID.ToString()),
                        new Claim(ClaimTypes.Role, newUser.UserPermissions.ToString())
                    };

                    await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "login")));

                    return RedirectToAction("Index", "Menus");
                }
                else
                {
                    return View(newUser); 
                }
            }
            return View(newUser);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}