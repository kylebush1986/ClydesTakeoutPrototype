using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using ClydesTakeoutPrototype.Data;
using ClydesTakeoutPrototype.Models.SystemModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace ClydesTakeoutPrototype.Controllers
{
    /// <summary>
    /// Controller providing access to Account functions such as Login and Sign Up.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ILocalDataContext _context;

        public AccountController(ILogger<AccountController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// GET Login View
        /// </summary>
        /// <returns>Login view</returns>
        public IActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        /// <summary>
        /// POST User from Login View for validation and sign in.
        /// </summary>
        /// <param name="currentUser">User class containing credentials posted from Login View</param>
        /// <returns>Redirect to Menu</returns>
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password,ID")] User currentUser)
        {
            if (ModelState.IsValid)
            {
                var dbUser = (from user in _context.UserDB
                              where (user.Email == currentUser.Email)
                              select user).FirstOrDefault();
                if (dbUser?.Password == Helpers.Utilities.GenerateDjb264Hash(currentUser.Password).ToString())
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, dbUser.FirstName),
                        new Claim(ClaimTypes.NameIdentifier, dbUser.ID.ToString()),
                        new Claim(ClaimTypes.Role, dbUser.UserPermissions.ToString())
                    };

                    await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "login")));

                    HttpContext.Session.Set("UserID", Encoding.ASCII.GetBytes(dbUser.ID.ToString()));

                    return RedirectToAction("Index", "Menus");
                }
            }
            return View(currentUser);
        }

        /// <summary>
        /// GET Sign Up View to create an account.
        /// </summary>
        /// <returns>Sign Up view</returns>
        public IActionResult SignUp()
        {
            User user = new User();
            return View(user);
        }

        /// <summary>
        /// POST Sign Up to create a new User account.
        /// </summary>
        /// <param name="newUser">New User credentials</param>
        /// <returns>Redirect to Menu</returns>
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
                    newUser.Password = Helpers.Utilities.GenerateDjb264Hash(newUser.Password).ToString();
                    _context.UserDB.Add(newUser);
                    _context.SaveDatabase(_context.UserDB);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, newUser.ID.ToString()),
                        new Claim(ClaimTypes.Role, newUser.UserPermissions.ToString())
                    };

                    HttpContext.Session.Set("UserID", Encoding.ASCII.GetBytes(newUser.ID.ToString()));

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

        /// <summary>
        /// GET Logout action to sign out current user.
        /// </summary>
        /// <returns>Redirect to Home</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// GET Unauthorized view if user does not have permission to access a resource.
        /// </summary>
        /// <returns>Unauthorized View</returns>
        public new IActionResult Unauthorized()
        {
            return View();
        }
    }
}